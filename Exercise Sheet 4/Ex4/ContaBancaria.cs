using System;
using System.Collections.Generic;
using System.Text;

namespace Ex4
{
    class ContaBancaria
    {
        private static UInt16 _numero;
        public static UInt16 Numero { get { return _numero; } }
        private UInt16 numero_conta;
        private Decimal saldo = 0.00M;
        private String nome;
        private Boolean estado = false;

        public Boolean Depositar(Decimal saldo)
        {
            if (saldo > 0 && (this.estado))
            {
                this.saldo += saldo;
                return true;
            }

            return false;
        }

        public Boolean Levantar(Decimal saldo)
        {
            if (saldo < (this.saldo - saldo) && (this.estado))
            {
                this.saldo -= saldo;
                return true;
            }

            return false;
        }

        public String Transferir(int numero_conta, Decimal saldo, List<ContaBancaria> x)
        {
            if ((UInt16)Math.Abs(numero_conta) <= _numero && this.numero_conta != (UInt16)Math.Abs(numero_conta))
            {
                if (x[numero_conta - 1].estado && this.estado)
                {
                    if (this.Levantar(saldo))
                    {
                        if (x[numero_conta - 1].Depositar(saldo))
                        {
                            return ("Valor " + saldo + " transferido com sucesso.");
                        }
                    }
                    
                }
            }

            return "Occureu um erro";
        }

        private ContaBancaria() { }

        public ContaBancaria(String nome, Decimal saldo)
        {
            _numero++;
            this.numero_conta = _numero;
            this.nome = nome;
            Depositar(saldo);
            this.estado = false;
        }

        public ContaBancaria(String nome, Decimal saldo, UInt16 num_conta, Boolean estado)
        {
            _numero++;
            this.nome = nome;
            this.saldo = saldo;
            this.numero_conta = num_conta;
            this.estado = estado;
        }
    }
}
