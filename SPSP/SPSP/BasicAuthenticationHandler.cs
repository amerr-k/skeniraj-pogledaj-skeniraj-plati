using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SPSP.Services.Employee;
using SPSP.Services.UserAccount;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SPSP
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        //IUserAccountService userAccountService;
        private readonly IEmployeeService employeeService;
        private readonly IConfiguration configuration;
        public BasicAuthenticationHandler(IEmployeeService employeeService, IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            this.employeeService = employeeService;
            this.configuration = configuration;
        }


        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing header");
            }

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            if (authHeader.Scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase))
            {
                return await HandleJwtAuthenticationAsync(authHeader.Parameter);

            }
            else if (authHeader.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return await HandleBasicAuthenticationAsync(authHeader.Parameter!);
            }

            return AuthenticateResult.Fail("Unauthorized");
        }

        private async Task<AuthenticateResult> HandleJwtAuthenticationAsync(string token)
        {
            var issuer = configuration.GetValue<string>("TokenConfig:Issuer");
            var audience = configuration.GetValue<string>("TokenConfig:Audience");
            var signingKey = configuration.GetValue<string>("TokenConfig:SigningKey");

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, 
                    ValidateAudience = true, 
                    ValidateIssuerSigningKey = true, 
                    ValidIssuer = issuer, 
                    ValidAudience = audience, 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)), 
                    ValidateLifetime = true, 
                    ClockSkew = TimeSpan.Zero 
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Failed to authenticate JWT token: " + ex.Message);
            }
        }

        protected async Task<AuthenticateResult> HandleBasicAuthenticationAsync(String authHeaderParameter)
        {

            var credentialsBytes = Convert.FromBase64String(authHeaderParameter);
            var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(':');

            var username = credentials[0];
            var password = credentials[1];

            var employee = await employeeService.GetAuthenticatedEmployee(username, password);

            if (employee == null || employee.UserAccount == null)
            {
                return AuthenticateResult.Fail("Incorrect username or password");
            }
            else
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, employee.UserAccount.FirstName),
                    new Claim(ClaimTypes.NameIdentifier, employee.UserAccount.Id.ToString())
                };

                foreach (var role in employee.UserAccount.UserAccountUserRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.UserRole.Name));
                }

                var identity = new ClaimsIdentity(claims, Scheme.Name);

                var principal = new ClaimsPrincipal(identity);

                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
        }
    }
}
