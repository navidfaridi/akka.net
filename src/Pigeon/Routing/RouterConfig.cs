﻿using Pigeon.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon.Routing
{
    public abstract class RouterConfig
    {
        public abstract RoutingLogic GetLogic();

        public abstract IEnumerable<Routee> GetRoutees(ActorSystem system);

        public static readonly RouterConfig NoRouter = new NoRouter();
    }

    public class NoRouter : RouterConfig
    {
        public override RoutingLogic GetLogic()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Routee> GetRoutees(ActorSystem system)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Group : RouterConfig
    {
        private string[] paths;

        public Group(IEnumerable<string> paths)
        {
            this.paths = paths.ToArray();
        }

        public override IEnumerable<Routee> GetRoutees(ActorSystem system)
        {
            foreach(var path in paths)
            {
                var actor = system.ActorSelection(path);
                yield return new ActorSelectionRoutee(actor);
            }
        }
    }
}
