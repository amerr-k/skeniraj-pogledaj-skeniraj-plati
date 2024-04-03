using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SPSP.Services.Database.SeedData
{
    public static class UserAccountData
    {
        public static void SeedData(this EntityTypeBuilder<UserAccount> entity)
        {
            entity.HasData(
                new UserAccount
                {
                    Id = 1,
                    Username = "admin",
                    PasswordSalt = "vJQCWBFgHnLmSr3p7vmTTQ==",
                    PasswordHash = "LzFTZiw5FRvxFRjjYTopRV3F0wQ=",
                    Email = "admin@gmail.com",
                    FirstName = "Edina",
                    LastName = "Kovač",
                    Registered = true,
                    Valid = true
                },
                new UserAccount
                {
                    Id = 2,
                    Username = "radnik",
                    PasswordSalt = "3u+BlGiNyzS/JQP3whaL+g==",
                    PasswordHash = "X8cce/4KENqQVJkNyQfnhBGyiP8=",
                    Email = "radnik@gmail.com",
                    FirstName = "Mehmedalija",
                    LastName = "Kovač",
                    Registered = true,
                    Valid = true
                },
                new UserAccount
                {
                    Id = 3,
                    Username = "kupac",
                    PasswordSalt = "p7Aa8JiK8YhF1xzdza4pEQ==",
                    PasswordHash = "Cu5kzgkqSh61OR+b8l6vmvN7PTE=",
                    Email = "kupac@gmail.com",
                    FirstName = "Amer",
                    LastName = "Kovač",
                    Registered = true,
                    Valid = true
                },
                new UserAccount
                {
                    Id = 4,
                    Username = "kupac2",
                    PasswordSalt = "BPVm5dgQVrNXUOyb34VCrg==",
                    PasswordHash = "H1MIG5uoEIBcJIbxnsfL7kZ8RgQ=",
                    Email = "kupac2@gmail.com",
                    FirstName = "Ishak",
                    LastName = "Kovač",
                    Registered = true,
                    Valid = true
                },
                new UserAccount
                {
                    Id = 5,
                    Username = "kupac3",
                    PasswordSalt = "test-ne-mozes-se-logovat",
                    PasswordHash = "test-ne-mozes-se-logovat",
                    Email = "kupac3@gmail.com",
                    FirstName = "Himzo",
                    LastName = "Polovina",
                    Registered = true,
                    Valid = true
                }
            );
        }
    }
}




