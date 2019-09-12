using System;

namespace Fleury.Agendamento.Domain.ValueObjects
{
    public class Valor
    {
        private readonly double _valor;

        public Valor(double valor)
        {
            this._valor = valor;
        }
                
        public static implicit operator Valor(int value)
        {
            return new Valor(value);
        }
        
        public static implicit operator double(Valor value)
        {
            return value._valor;
        }

        public static implicit operator Valor(double value)
        {
            return new Valor(value);
        }

        public static Valor operator +(Valor Valor1, Valor Valor2)
        {
            return new Valor(Valor1._valor + Valor2._valor);
        }

        public static Valor operator -(Valor Valor1, Valor Valor2)
        {
            return new Valor(Valor1._valor - Valor2._valor);
        }

        public static bool operator <(Valor Valor1, Valor Valor2)
        {
            return Valor1._valor < Valor2._valor;
        }

        public void Sould()
        {
            throw new NotImplementedException();
        }

        public static bool operator >(Valor Valor1, Valor Valor2)
        {
            return Valor1._valor > Valor2._valor;
        }

        public static bool operator <=(Valor Valor1, Valor Valor2)
        {
            return Valor1._valor <= Valor2._valor;
        }

        public static bool operator >=(Valor Valor1, Valor Valor2)
        {
            return Valor1._valor >= Valor2._valor;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is double)
            {
                return (double)obj == _valor;
            }

            return ((Valor)obj)._valor == _valor;
        }

    }
}
