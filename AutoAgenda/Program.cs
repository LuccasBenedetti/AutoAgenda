
namespace AutoAgenda {
    class Paciente {
        public string Nome { get; set; }
        public int Duracao { get; set; }

        public Paciente(string nome, int duracao) {
            Nome = nome;
            Duracao = duracao;
        }
    }

    class Program {

        static void Main(string[] args) {

            //Montar os horários disponíveis nos dias Segunda, Quarta e Sexta
            TimeSpan inicioPrimeiro, fimPrimeiro, inicioSegundo, fimSegundo, inicioTerceiro, fimTerceiro;

            double totalPrimeiro, totalSegundo, totalTerceiro, totalDisponivel;


            Console.WriteLine("Insira o horário de início da Segunda Feira (formato desejado: 13:00): ");
            inicioPrimeiro = TimeSpan.Parse(Console.ReadLine()!);
            Console.WriteLine("Insira o horário de término da Segunda Feira: ");
            fimPrimeiro = TimeSpan.Parse(Console.ReadLine()!);
            var duracaoPrimeiro = fimPrimeiro - inicioPrimeiro;
            totalPrimeiro = duracaoPrimeiro.TotalMinutes;
            Console.WriteLine("Duração em minutos da Segunda Feira: " + totalPrimeiro + " minutos");


            Console.WriteLine("Insira o horário de início da Quarta Feira: ");
            inicioSegundo = TimeSpan.Parse(Console.ReadLine()!);
            Console.WriteLine("Insira o horário de término da Quarta Feira: ");
            fimSegundo = TimeSpan.Parse(Console.ReadLine()!);
            var duracaoSegundo = fimSegundo - inicioSegundo;
            totalSegundo = duracaoSegundo.TotalMinutes;
            Console.WriteLine("Duração em minutos da Quarta Feira: " + totalSegundo + " minutos");


            Console.WriteLine("Insira o horário de início da Sexta Feira: ");
            inicioTerceiro = TimeSpan.Parse(Console.ReadLine()!);
            Console.WriteLine("Insira o horário de término da Sexta Feira: ");
            fimTerceiro = TimeSpan.Parse(Console.ReadLine()!);
            var duracaoTerceiro = fimTerceiro - inicioTerceiro;
            totalTerceiro = duracaoTerceiro.TotalMinutes;
            Console.WriteLine("Duração em minutos da Sexta Feira: " + totalTerceiro + " minutos");

            totalDisponivel = totalPrimeiro + totalSegundo + totalTerceiro;
            Console.WriteLine("Total para agendamento: " + totalDisponivel + " minutos");

            List<Paciente> pacientes = new List<Paciente>();

            //Obter os nomes e durações das consultas dos pacientes
            for (int i = 1; i <= 9; i++) {
                Console.WriteLine("Digite o nome do paciente " + i + ":");
                string nome = Console.ReadLine()!;

                Console.WriteLine("Digite a duração da consulta do paciente " + i + " (em minutos):");
                int duracao = int.Parse(Console.ReadLine()!);

                Paciente paciente = new Paciente(nome, duracao);
                pacientes.Add(paciente);
            }

            //Ordernar a lista de pacientes em ordem decrescente de duração das consultas
            pacientes.Sort((p1, p2) => p2.Duracao.CompareTo(p1.Duracao));

            //Variáveis pra armazenar os pacientes em cada dia
            List<Paciente> dia1 = new List<Paciente>();
            List<Paciente> dia2 = new List<Paciente>();
            List<Paciente> dia3 = new List<Paciente>();

            // Encaixar os pacientes nos dias disponíveis
            foreach (Paciente paciente in pacientes) {
                if (AgendarConsulta(dia1, paciente, (int)totalPrimeiro)) {
                    continue;
                }

                if (AgendarConsulta(dia2, paciente, (int)totalSegundo)) {
                    continue;
                }

                if (AgendarConsulta(dia3, paciente, (int)totalTerceiro)) {
                    continue;
                }

                Console.WriteLine("Não foi possível encaixar a consulta do paciente " + paciente.Nome + ".");
            }

            // Imprimir o agendamento dos pacientes por dia
            Console.WriteLine("Agendamento dos pacientes:");

            Console.WriteLine("Segunda:");
            foreach (Paciente paciente in dia1) {
                Console.WriteLine("Paciente: " + paciente.Nome + ", Duração: " + paciente.Duracao + " minutos");
            }

            Console.WriteLine("Quarta:");
            foreach (Paciente paciente in dia2) {
                Console.WriteLine("Paciente: " + paciente.Nome + ", Duração: " + paciente.Duracao + " minutos");
            }

            Console.WriteLine("Sexta:");
            foreach (Paciente paciente in dia3) {
                Console.WriteLine("Paciente: " + paciente.Nome + ", Duração: " + paciente.Duracao + " minutos");
            }
        }

        static bool AgendarConsulta(List<Paciente> dia, Paciente paciente, int duracaoMaxima) {
            int duracaoRestante = duracaoMaxima;

            foreach (Paciente p in dia) {
                duracaoRestante -= p.Duracao;
            }

            if (duracaoRestante >= paciente.Duracao) {
                dia.Add(paciente);
                return true;
            }

            return false;
        }
    }
}