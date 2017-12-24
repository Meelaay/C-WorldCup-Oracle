using System;
using System.Windows.Forms;

namespace RepoRepo
{
    public class Match
    {
        //private int _matchID; -> could be used later to identify matches or maybe use just names

        public DateTime MatchDate { get; } //<- check if datetime or timespan or right type ?
        public BasicTeam Team1 { get; }
        public BasicTeam Team2 { get; }

        public readonly Label DateLabel = new Label();

        public TextBox score1 { get; }
        public TextBox score2 { get; }



        public void SetResultToTeam()
        {
            int score1Int = Convert.ToInt32(score1.Text);
            int score2Int = Convert.ToInt32(score2.Text);

            if (score1Int != score2Int)
            {
                if (score1Int > score2Int)
                {
                    Team1.points += 3; Team1.win++; Team2.loss++;
                }
                else
                {
                    Team2.points += 3; Team2.win++; Team1.loss++;
                }
            }
            else
            {
                Team1.points++; Team1.draw++;
                Team2.points++; Team2.draw++;
            }
        }

        public Match(BasicTeam team1, BasicTeam team2, DateTime matchDate)
        {
            Team1 = BasicTeam.DeepCopyTeam(team1);
            Team2 = BasicTeam.DeepCopyTeam(team2);
            score1 = new TextBox();
            score2 = new TextBox();
            MatchDate = matchDate;
            DateLabel.AutoSize = true;
            DateLabel.Text = matchDate.ToLongDateString();

            score1.Text = "";
            score2.Text = "";

            score1.Width = score2.Width = 20;
            score1.Font = score2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        public void MakeFieldsReadOnly()
        {
            score1.ReadOnly = true;
            score2.ReadOnly = true;
        }

        

    }
}