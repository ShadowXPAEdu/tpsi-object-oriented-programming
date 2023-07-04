using System;
using System.Collections.Generic;
using System.Text;

namespace Ex2
{
    public enum Combustivel
    {
        Diesel,
        Gasolina,
        GPL,
        Electrico,
        Hidrogenio,
        Hybrid
    }

    class Automovel
    {
        private String matricula, marca, modelo, cor;
        private int velocidade, quantDeposito, quantDepositoMax, velMax;
        private Combustivel comb;

        public void Trava(int quant)
        {
            if (quant > 0 && this.velocidade > 0)
                this.velocidade -= quant;
        }

        public void Acelera(int quant)
        {
            if (quant > 0)
            {
                if (this.velocidade + quant > this.velMax)
                    this.velocidade = velMax;
                else
                    this.velocidade += quant;
            }
        }

        public String Abastecer(Combustivel tipo, int quant)
        {
            if (tipo == this.comb)
            {
                if (this.quantDepositoMax < (this.quantDeposito + quant))
                {
                    return "Quantidade maior que o depósito";
                }
                else
                {
                    this.quantDeposito += quant;
                    return "";
                }
            }
            return "Tipo de Combustível errado";
        }

        // Getters
        public String GetMatricula()
        {
            return this.matricula;
        }

        public String GetMarca()
        {
            return this.marca;
        }

        public String GetModelo()
        {
            return this.modelo;
        }

        public String GetCor()
        {
            return this.cor;
        }

        public int GetVelocidade()
        {
            return this.velocidade;
        }

        public Combustivel GetCombustivel()
        {
            return this.comb;
        }

        public int GetVelMax()
        {
            return this.velMax;
        }

        public int GetQuantDep()
        {
            return this.quantDeposito;
        }

        public int GetQuantDepMax()
        {
            return this.quantDepositoMax;
        }

        // Setters
        public void SetVel(int vel)
        {
            if (vel < 0)
            {
                this.velocidade = 0;
            }
            else if (vel > this.velMax)
            {
                this.velocidade = this.velMax;
            }
            else
            {
                this.velocidade = vel;
            }
        }

        public String SetQuantDep(int dep)
        {
            if (this.quantDepositoMax < (dep + this.quantDeposito))
            {
                return "Imposível depositar (quantidade a depositar + quantidade atual no depósito maior que o máximo depósito possível)";
            }
            else
            {
                this.quantDeposito += dep;
                return "Success";
            }
        }

        public void SetQuantDepMax(int depMax)
        {
            this.quantDepositoMax = depMax;
        }

        // Constructors
        private Automovel() { }

        public Automovel(String matricula, String marca, String modelo, String cor, int velMax, Combustivel comb)
        {
            this.matricula = matricula;
            this.marca = marca;
            this.modelo = modelo;
            this.cor = cor;
            this.velMax = velMax;
            this.comb = comb;

            this.velocidade = 0;
            this.quantDeposito = 0;
        }
    }
}
