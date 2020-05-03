using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;

        public ValueOutOfRangeException(string i_ErrorMsg, float i_MinimumValue, float i_MaximumValue) : base(i_ErrorMsg)
        {
            m_MinValue = i_MinimumValue;
            m_MaxValue = i_MaximumValue;
        }

        public override string ToString()
        {
            return string.Format("{0} must be between {1:f} and {2:f}, please try again.", Message, m_MinValue, m_MaxValue);
        }
    }
}
