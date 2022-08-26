using System;
using System.Collections.Generic;
using System.Text;

namespace Ex1
{
    class Pessoa
    {
        private String nome, morada;
        private DateTime dataNasc;
        private char sexo;
        private int numTelefone, CC;

        // Getters
        public String GetName()
        {
            return this.nome;
        }

        public String GetMorada()
        {
            return this.morada;
        }

        public int GetIdade()
        {
            if (DateTime.Now.Month >= this.dataNasc.Month)
                return (DateTime.Now.Year - this.dataNasc.Year);

            if ((DateTime.Now.Month >= this.dataNasc.Month) && (DateTime.Now.Hour >= this.dataNasc.Hour))
                return (DateTime.Now.Year - this.dataNasc.Year);

            if ((DateTime.Now.Month >= this.dataNasc.Month) && (DateTime.Now.Day >= this.dataNasc.Day) && (DateTime.Now.Hour >= this.dataNasc.Hour))
                return (DateTime.Now.Year - this.dataNasc.Year);

            if ((DateTime.Now.Month >= this.dataNasc.Month) && (DateTime.Now.Day >= this.dataNasc.Day) && (DateTime.Now.Hour >= this.dataNasc.Hour) && (DateTime.Now.Minute >= this.dataNasc.Minute))
                return (DateTime.Now.Year - this.dataNasc.Year);

			if ((DateTime.Now.Month >= this.dataNasc.Month) && (DateTime.Now.Day >= this.dataNasc.Day) && (DateTime.Now.Hour >= this.dataNasc.Hour) && (DateTime.Now.Minute >= this.dataNasc.Minute) && (DateTime.Now.Second >= this.dataNasc.Second))
				return (DateTime.Now.Year - this.dataNasc.Year);

            return (DateTime.Now.Year - this.dataNasc.Year) - 1);
        }

        public DateTime GetDataNasc()
        {
            return this.dataNasc;
        }

        public char GetSexo()
        {
            return this.sexo;
        }

        public int GetNumTelefone()
        {
            return this.numTelefone;
        }

        public int GetCC()
        {
            return this.CC;
        }

        // Setters
        public void SetName(String nome)
        {
            this.nome = nome;
        }

        public void SetMorada(String morada)
        {
            this.morada = morada;
        }

        public void SetNumTel(int num)
        {
            this.numTelefone = num;
        }

        public void SetSexo(char sex)
        {
            this.sexo = sex;
        }

        // Constructors
        private Pessoa() {}

        public Pessoa(String nome, DateTime data, int CC, char sexo)
        {
            this.nome = nome;
            this.dataNasc = data;
            this.CC = CC;
            this.sexo = sexo;
        }
    }
}
