using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeanEngine.Utility
{
    /// <summary>
    /// Define Enum types
    /// </summary>
    public static class Enumerators
    {
        /// <summary>
        /// Procurement: purchase
        /// Distribution: sales
        /// </summary>
        public enum FlowType
        {
            Procurement,
            Production,
            Distribution,
            Transfer
        }

        /// <summary>
        /// Issue or receipt?
        /// </summary>
        public enum IRType
        {
            ISS,
            RCT
        }

        /// <summary>
        /// Orders is confirmed demand and will be executed, but Plans is not
        /// </summary>
        public enum PlanType
        {
            Plans,
            Orders
        }

        /// <summary>
        /// Flow strategy:KB/JIT
        /// </summary>
        public enum Strategy
        {
            /// <summary>
            /// Manual
            /// </summary>
            Manual,
            /// <summary>
            /// Kanban
            /// </summary>
            KB,
            /// <summary>
            /// Order Point Method
            /// </summary>
            ODP,
            /// <summary>
            /// Just In Time
            /// </summary>
            JIT,
            /// <summary>
            /// Material Requirement Planning
            /// </summary>
            MRP
        }

        /// <summary>
        /// Time unit:hours/day/week/month
        /// </summary>
        public enum TimeUnit
        {
            /// <summary>
            /// Default:hours
            /// </summary>
            Default,
            Day,
            Week,
            Month,
            Quarter,
            Year
        }

        /// <summary>
        /// Round up option
        /// </summary>
        public enum RoundUp
        {
            None,
            Ceiling,
            Floor
        }

        /// <summary>
        /// Inventory type
        /// </summary>
        public enum InvType
        {
            Normal,
            Inspect
        }

        /// <summary>
        /// Order Tracer Type
        /// </summary>
        public enum TracerType
        {
            Demand,
            OnhandInv,
            InspectInv,
            OrderRct,
            OrderIss,
            PlanRct,
            PlanIss
        }
    }
}
