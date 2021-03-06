﻿namespace Endpoint
{
	using System;
	using Messages.Commands;
	using NServiceBus;
	using Raven.Client;
	using Raven.Client.Document;

    public class MyCommandHandler : IHandleMessages<IMyCommand>
	{
        public IDocumentSession Session { get; set; }

		public void Handle(IMyCommand message)
		{
            Console.WriteLine("Handeling a MyCommand of message type: {1} with Id {0}."
					, message.IdGuid, message.GetType());

            // do some work with Raven using MyRavenUnitOfWork
            Console.WriteLine("Session Id:({0}) - Saving MyCommand to raven", ((DocumentSession)Session).Id);
			Console.WriteLine("==========================================================================");

            Session.Store(new CommandData
            {
                IdGuid = message.IdGuid,
                Name = message.Name
            });

            if (message.Throw) throw new Exception("Ho nos, MyCommand, we have an issue...");
        }
	}
}
