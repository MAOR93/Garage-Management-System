using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected readonly float r_MaxEnergy;
        protected float m_AmountOfLeftEnergy;
        private readonly float r_MinEnergy = 1f;

        public Engine(float i_MaxEnergy)
        {
            r_MaxEnergy = i_MaxEnergy;
        }

        public float MaxEnergy
        {
            get
            {
                return r_MaxEnergy;
            }
        }

        public float AmountOfLeftEnergy
        {
            get
            {
                return m_AmountOfLeftEnergy;
            }

            set
            {
                if (value > r_MinEnergy && value <= r_MaxEnergy)
                {
                    m_AmountOfLeftEnergy = value;
                }
                else
                {
                    throw new ValueOutOfRangeException("Error - The energy must be between ", 1, r_MaxEnergy);
                }
            }
        }

        public void SetEnergyByPercentage(float i_PercentageEnergyToAdd)
        {
            m_AmountOfLeftEnergy = (i_PercentageEnergyToAdd / 100) * r_MaxEnergy;
        }

        public override string ToString()
        {
            StringBuilder engineInfo = new StringBuilder();
            engineInfo.Append(string.Format("The max possible energy is: {0:f}{1}", r_MaxEnergy, Environment.NewLine));
            engineInfo.Append(string.Format("The current energy is: {0:f}{1}", m_AmountOfLeftEnergy, Environment.NewLine));
            return engineInfo.ToString();
        }
    }
}
