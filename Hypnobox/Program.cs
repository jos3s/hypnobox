namespace Hypnobox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Entre com os com os gols do Time A (separados por vírgula):");
            var inputTeamA = Console.ReadLine();
            Console.WriteLine("Entre com os com os gols do Time B (separados por vírgula):");
            var inputTeamB = Console.ReadLine();

            if (inputTeamA == null || inputTeamB == null)
                throw new Exception("Os valores do Time A ou Time B não podem ser nulos.");

            var goalsTeamA = inputTeamA.Split(',').ToList();
            var goalsTeamB = inputTeamB.Split(',').ToList();

            var teamBGoalsMoreTeamA = new List<string>();
            goalsTeamB.ForEach(goalB =>
            {
                int sum = 0;
                foreach (var goalA in goalsTeamA)
                {
                    if(int.Parse(goalB) >= int.Parse(goalA))
                        sum++;
                }
                teamBGoalsMoreTeamA.Add(sum.ToString());
            });


            Console.WriteLine("O time B ganhou:");
            for (int i = 0; i < teamBGoalsMoreTeamA.Count; i++)
            {
                Console.WriteLine($"A {i+1} partida ganhou de {teamBGoalsMoreTeamA[i].ToString()} partidas do time A");
            }
        }
    }
}