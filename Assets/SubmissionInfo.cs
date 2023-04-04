
// This class contains metadata for your submission. It plugs into some of our
// grading tools to extract your game/team details. Ensure all Gradescope tests
// pass when submitting, as these do some basic checks of this file.
public static class SubmissionInfo
{
    // TASK: Fill out all team + team member details below by replacing the
    // content of the strings. Also ensure you read the specification carefully
    // for extra details related to use of this file.

    // URL to your group's project 2 repository on GitHub.
    public static readonly string RepoURL = "https://github.com/COMP30019/project-2-team-ras";
    
    // Come up with a team name below (plain text, no more than 50 chars).
    public static readonly string TeamName = "Team RAS";
    
    // List every team member below. Ensure student names/emails match official
    // UniMelb records exactly (e.g. avoid nicknames or aliases).
    public static readonly TeamMember[] Team = new[]
    {
        new TeamMember("Raziel Maron", "rmaron@student.unimelb.edu.au"),
        new TeamMember("Anthony Ouch", "aouch@student.unimelb.edu.au"),
        new TeamMember("Sandeepa Andra Hennadige", "sandrahennad@student.unimelb.edu.au"),

    };

    // This may be a "working title" to begin with, but ensure it is final by
    // the video milestone deadline (plain text, no more than 50 chars).
    public static readonly string GameName = "Escape the Labyrinth";

    // Write a brief blurb of your game, no more than 200 words. Again, ensure
    // this is final by the video milestone deadline.
    public static readonly string GameBlurb = 
@"Choose your adventurer to Escape the Labyrinth with. 
The Labyrinth holds a myriad of floors which require you to both fight and avoid monsters. 
The player can find and collect weapons and items which will make you stronger and help you in your quest to escape, 
whilst some items may deceive and harm you. Keep a look out for traps and beware, 
the Labyrinth will get harder as you get closer to escaping. Good Luck!
";
    
    // By the gameplay video milestone deadline this should be a direct link
    // to a YouTube video upload containing your video. Ensure "Made for kids"
    // is turned off in the video settings. 
    public static readonly string GameplayVideo = "https://www.youtube.com/watch?v=NiDdIfUeF0s";
    
    // No more info to fill out!
    // Please don't modify anything below here.
    public readonly struct TeamMember
    {
        public TeamMember(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}
