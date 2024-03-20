﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using SPSP.Models.Request.Reservation;
using SPSP.Services.Database;
using SPSP.Services.Reservation.StateMachine.Generics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPSP.Services.Reservation.StateMachine
{
    public class PendingConfirmationReservationState
        : BaseState
    {
        protected ILogger<PendingConfirmationReservationState> logger;
        public PendingConfirmationReservationState(
            ILogger<PendingConfirmationReservationState> logger,
            IServiceProvider serviceProvider,
            DataDbContext context,
            IMapper mapper)
            : base(serviceProvider, context, mapper)
        {
            this.logger = logger;
        }

        public override async Task<Models.Reservation> Update(Database.Reservation dbEntity, ReservationUpdateRequest update)
        {
            //nema potrebe da se ponovo trazi entity, odma se moze poslat u metodu
            //var set = context.Set<Database.Reservation>();
            //var entity = await set.FindAsync(id);

            mapper.Map(update, dbEntity);

            await context.SaveChangesAsync();

            return mapper.Map<Models.Reservation>(dbEntity);


        }

        public override async Task<Models.Reservation> ConfirmReservation(int id)
        {
            logger.LogInformation($"Konfirmacija rezervacije id: {id}");

            var set = context.Set<Database.Reservation>();

            var entity = await set.FindAsync(id);

            entity.Status = "CONFIRMED";

            await context.SaveChangesAsync();

            return mapper.Map<Models.Reservation>(entity);
        }

        public override async Task<Models.Reservation> CancelReservation(int id)
        {
            var set = context.Set<Database.Reservation>();

            var entity = await set.FindAsync(id);

            entity.Status = "CANCELED";

            await context.SaveChangesAsync();

            return mapper.Map<Models.Reservation>(entity);
        }

        public override async Task<List<string>> GetAllowedActions()
        {
            var allowedActions = await base.GetAllowedActions();
            allowedActions.AddRange(
                new List<string> { "Update", "ConfirmReservation", "Cancel"}); //mogu li se ovdje staviti create i put on hold ili cu u create-u radit provjeru?
            return allowedActions;
        }
    }
}
