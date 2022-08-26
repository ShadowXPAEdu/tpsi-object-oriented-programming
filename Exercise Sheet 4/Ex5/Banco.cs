using System;
using System.Collections.Generic;
using System.Text;

namespace Ex5
{
    class Banco
    {
        private static UInt16 _num_banco;
        public static UInt16 Numero_Banco { get { return _num_banco; } }
        private List<ContaBancaria> contas;
        private String nome, morada;
        private UInt16 num_banco;

        public int GetNumeroDeContas()
        {
            return this.contas.Count;
        }

        public ContaBancaria GetContaBancaria(UInt16 num_conta)
        {
            return this.contas[num_conta - 1];
        }

        public Boolean CreateContaBancaria(String nome, Decimal saldo, Boolean estado)
        {
            if (nome != "" || nome != null)
                if (saldo >= 0.0M)
                {
                    ContaBancaria novaConta = new ContaBancaria(nome, saldo, estado);
                    AddContaBancaria(novaConta);
                    return true;
                }
            return false;
        }

        public Boolean DeleteContaBancaria(UInt16 num_conta)
        {
            if (num_conta > 0 && num_conta < this.contas.Count)
            {
                RemoveContaBancaria(num_conta);
                return true;
            }

            return false;
        }

        public void RemoveContaBancaria(UInt16 num_conta)
        {
            this.contas.Remove(GetContaBancaria(num_conta));
        }

        public void AddContaBancaria(ContaBancaria conta)
        {
            this.contas.Add(conta);
        }

        public Banco(String nome, String morada, List<ContaBancaria> contas)
        {
            num_banco = _num_banco++;
            this.nome = nome;
            this.morada = morada;
            if (contas != null)
                this.contas = new List<ContaBancaria>(contas);
            this.contas = new List<ContaBancaria>();
        }
        
        public Banco(String nome, String morada) : this(nome, morada, null)
        {
        }

        private Banco() : this("Banco" + _num_banco, "Morada" + _num_banco)
        {
        }

        static Banco()
        {
            _num_banco = 1;
        }
    }
}
