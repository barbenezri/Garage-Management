using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public float MaxValue
        {
            get => m_MaxValue;
            private set => m_MaxValue = value;
        }

        public float MinValue
        {
            get => m_MinValue;
            private set => m_MinValue = value;
        }

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue, string i_Message)
            : base(i_Message)
        {
            MaxValue = i_MaxValue;
            MinValue = i_MinValue;
        }
    }
}
