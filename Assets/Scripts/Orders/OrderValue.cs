using System;

namespace PizzaGame.Orders
{
    /// <summary>
    /// Represents the value of an order
    /// </summary>
    [Serializable]
    public struct OrderValue: IEquatable<OrderValue>
    {
        /// <summary>
        /// Dollars in order value
        /// </summary>
        public int Dollars;

        /// <summary>
        /// Cents in order value
        /// </summary>
        public int Cents;

        private const int HUNDRED = 100;

        /// <summary>
        /// Construct an order value
        /// </summary>
        /// <param name="dollars">dollars</param>
        /// <param name="cents">cents</param>
        public OrderValue(int dollars, int cents)
        {
            this.Dollars = dollars;
            this.Cents = cents;
        }

        /// <summary>
        /// Construct an order value from a total value
        /// </summary>
        /// <param name="totalValue">the total value</param>
        public OrderValue(int totalValue)
        {
            this.Dollars = totalValue / HUNDRED;
            this.Cents = totalValue % HUNDRED;
        }

        public override string ToString()
        {
            return string.Format("{0:C}", (double)this.Dollars + (double)this.Cents / HUNDRED);
        }

        public bool Equals(OrderValue other)
        {
            return this == other;
        }

        public override bool Equals(object other)
        {
            if (other is OrderValue)
            {
                return this == (OrderValue) other;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Dollars.GetHashCode() & this.Cents.GetHashCode();
        }

        public static OrderValue operator +(OrderValue a, OrderValue b)
        {
            int newCents = (a.Cents + b.Cents) % HUNDRED;
            int newDollars = a.Dollars + b.Dollars + ((a.Cents + b.Cents) / HUNDRED);

            return new OrderValue(newDollars, newCents);
        }

        public static bool operator ==(OrderValue a, OrderValue b)
        {
            return a.Dollars == b.Dollars && a.Cents == b.Cents;
        }

        public static bool operator !=(OrderValue a, OrderValue b)
        {
            return !(a == b);
        }

        public static bool operator >(OrderValue a, OrderValue b)
        {
            if (a.Dollars > b.Dollars)
            {
                return true;
            }

            return a.Cents > b.Cents;
        }

        public static bool operator <(OrderValue a, OrderValue b)
        {
            if (a.Dollars < b.Dollars)
            {
                return true;
            }

            return a.Cents < b.Cents;
        }
    }
}
