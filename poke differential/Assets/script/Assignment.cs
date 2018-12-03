/// <summary>
/// Denne klasse skal kunne lave og rette opgaver.
/// Den skal samtidig styre progressionen og sværhedsgraden i opgaverne
/// </summary>

public class Assignment {
    #region Variables
    // Det korrekte svar til den nuværende opgave
    public string correctAnswer;

    // Angiver # af opgaver, brugeren har svaret rigtigt på
    private int correctAnswers;

    // Kan returnere # af opgaver, brugeren har svaret rigtigt på
    public int GetCorrectAnswers() { return correctAnswers; }

    // Angiver hvilken orden, som polynomiet er, fx lineær/parabel
    private int orderOfFunc;

    // Angiver brugerens nuværende ekspertiseniveau
    private Difficulty diff;

    // Antal rigtige svar i streg
    private int correctAnswersInARow;

    // Antal forkerte svar i streg
    private int wrongAnswersInARow;

    // Indeks af den uafhængige variabel i funktionsforskriften
    private int[] posOfVar = new int[9];

    // Angiver forskellige sværhedsgrader i opgaverne
    public enum Difficulty
    {
        INTRODUCTION,
        EASY,
        MEDIUM,
        HARD,
        INSANE
    };
    #endregion
    #region Constructors
    // Nulstiller klassevariablene
    private void Initialize()
    {
        correctAnswer = "";
        correctAnswers = 0;
        orderOfFunc = 0;
        correctAnswersInARow = 0;
        wrongAnswersInARow = 0;
    }

    // Constructor med en given sværhedsgrad til opgaverne
    public Assignment(Difficulty difficulty)
    {
        Initialize();
        diff = difficulty;
    }

    // Default constructor
    public Assignment()
    {
        Initialize();
        diff = Difficulty.INTRODUCTION;
    }
    #endregion
    #region Generate functions
    // Gør funktionen mere læsevenlig for brugeren ved at
    // fjerne irrelevant information i funktionsudtrykket
    private string CleanGenerated(string function)
    {
        function = function.Replace("x^1", "x");
        function = function.Replace("x^0", "");
        return function;
    }

    // Kan generere et polynomie
    // int lengthOfPolyonimal angiver hvilken orden, polynomiet er
    private string Generate(int lengthOfPolynomial)
    {
        string task = ""; // Nuværende opgave
        System.Random rand = new System.Random(); // Random Number Generator
        for (int i = 0; i <= lengthOfPolynomial; i++)
        {
            // Sæt fortegn på første led
            if (i == 0 && rand.Next(2) == 1)
                task += "-";

            // Genererer tallet foran variablen
            if (diff == Difficulty.INSANE || diff == Difficulty.HARD)
                task += rand.Next(1, 15);
            else
                task += rand.Next(1, 5);

            // Genererer den uafhængige variabel og eksponenten
            task += "x^" + (lengthOfPolynomial - i);

            // Sæt '+' eller '-' ind i funktionsforskriften
            if (i != lengthOfPolynomial)
            {
                if (rand.Next(2) == 1)
                    task += "+";
                else
                    task += "-";
            }
        }
        return task;
    }

    // Kan lave opgaver til brugeren. Opgaverne afhænger af
    // brugerens ekspertiseniveau
    public string Create()
    {
        // Ændrer sværhedsgraden i opgaverne
        if (correctAnswersInARow > 3)
            LowerDifficulty(false);
        if (wrongAnswersInARow > 3)
            LowerDifficulty(true);

        string task = ""; // Opgaven, som brugeren får
        string function = ""; // Den genererede funktion
        System.Random rand = new System.Random(); // Random Number Generator

        // Afhængig af den nuværende sværhedsgrad, så generer en relevant opgave
        switch (diff)
        {
            case Difficulty.INTRODUCTION:
                task = Introduction();
                break;

            case Difficulty.EASY:
                function = Generate(2);
                task = DifferentiateAssignment(function);
                break;

            case Difficulty.MEDIUM:
                function = Generate(3);
                task = DifferentiateAssignment(function);
                break;

            case Difficulty.HARD:
                if (rand.Next(2) == 1)
                {
                    function = Generate(3);
                    task = DifferentiateAssignment(function);
                }
                else
                {
                    function = Generate(2);
                    task = FindExtremaAssignment(function);
                }
                break;

            case Difficulty.INSANE:
                if (rand.Next(2) == 1)
                {
                    function = Generate(4);
                    task = DifferentiateAssignment(function);
                }
                else
                {
                    function = Generate(2);
                    task = FindExtremaAssignment(function);
                }
                break;
        }
        return CleanGenerated(task);
    }

    // Sætter correctAnswer klassevariabel til det rigtige og returnerer
    // en tekststreng, som kan give opgaven til brugeren
    // string function er funktionsforskriften
    private string DifferentiateAssignment(string function)
    {
        correctAnswer = CleanGenerated(Differentiate(function));
        return "Du bedes aflede funktionen: " + function;
    }

    // Sætter correctAnswer klassevariabel til det rigtige og returnerer
    // en tekststreng, som kan give opgaven til brugeren
    // string function er funktionsforskriften
    private string FindExtremaAssignment(string function)
    {
        correctAnswer = "X: " + CalculateExtrema(function, 'x')
                     + " Y: " + CalculateExtrema(function, 'y');
        return "Du bedes finde ekstremumspunkt på funktionen: " + function;
    }

    // Funktion, der kan opstille de forudinstillede opgaver, som brugeren
    // vil møde i repetitionsniveauet i spillet, dvs. det er undervisningsforløbet
    private string Introduction()
    {
        string task = ""; // Nuværende opgave
        string function = ""; // Funktionen, der arbejdes med

        // Giv brugeren en fast opgave afhængig af, hvor
        // langt brugeren er nået i uddannelsesforløbet
        switch (correctAnswers)
        {
            case 0:
                function = "3x^1+4x^0";
                task = DifferentiateAssignment(function);
                break;
            case 1:
                function = "8x^1-2x^0";
                task = DifferentiateAssignment(function);
                break;
            case 2:
                function = "5x^2+3x^1-4x^0";
                task = DifferentiateAssignment(function);
                break;
                // TO BE CONTINUED
        }
        return task;
    }

    // Kan hæve/sænke opgavernes sværhedsgrad
    // bool lowerDifficulty angiver, hvor vidt sværhedsgraden skal hæves eller sænkes
    private void LowerDifficulty(bool lowerDifficulty)
    {
        if (lowerDifficulty)
            DecreaseDifficulty(diff);
        else
            IncreaseDifficulty(diff);
    }

    // Hæver sværhedsgraden af opgaverne
    private void IncreaseDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.INTRODUCTION:
                diff = Difficulty.EASY;
                break;
            case Difficulty.EASY:
                diff = Difficulty.MEDIUM;
                break;
            case Difficulty.MEDIUM:
                diff = Difficulty.HARD;
                break;
            case Difficulty.HARD:
                diff = Difficulty.INSANE;
                break;
            case Difficulty.INSANE:
                diff = Difficulty.INSANE;
                break;
        }
    }

    // Sænker sværhedsgraden af opgaverne
    private void DecreaseDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.INTRODUCTION:
                diff = Difficulty.INTRODUCTION;
                break;
            case Difficulty.EASY:
                diff = Difficulty.EASY;
                break;
            case Difficulty.MEDIUM:
                diff = Difficulty.EASY;
                break;
            case Difficulty.HARD:
                diff = Difficulty.MEDIUM;
                break;
            case Difficulty.INSANE:
                diff = Difficulty.HARD;
                break;
        }
    }
    #endregion
    #region Analyze functions
    // Angiver, om polynomiet har et ekstremumspunkt
    private bool HasExtrema()
    {
        // Funktionen har et toppunkt, hvis det er en 2. ordens
        // funktion, dvs. en parabel eller større orden
        if (orderOfFunc >= 2)
            return true;
        else
            return false;
    }

    // Kan beregne ekstremumspunktet, dvs. top- eller minimumspunkt
    // string function er den givne funktion
    // char axis er hvilken akse, der beregnes toppunkt ved, enten 'x' eller 'y'.
    private string CalculateExtrema(string function, char axis)
    {
        // Leder efter indeks, hvor funktionforskriften har 
        // variablen 'x' i sig
        SearchForChar(function, 'x');

        // Tjekker om funktionen overhovedet har et toppunkt
        if (HasExtrema())
        {
            // a, b, c er led i 2. grads polynomiet
            // Konverterer fra int til float for at få decimaltal med
            float a = System.Convert.ToSingle(GetConstantOfFunction(function, 'a'));
            float b = System.Convert.ToSingle(GetConstantOfFunction(function, 'b'));
            if (axis == 'x')
            {
                // Formel: x = (-b) / (2 * a)
                float res = (-b) / (2 * a);
                // Konverterer til string for at evt. afkorte x-koordinatet
                return res.ToString("0.#");
            }
            else if (axis == 'y')
            {
                // Formel: y = -(b ^ 2 - 4 * a * c) / (4 * a)
                float c = System.Convert.ToSingle(GetConstantOfFunction(function, 'c'));
                float res = -(b * b - 4 * a * c) / (4 * a);
                // Konverterer til string for at evt. afkorte x-koordinatet
                return res.ToString("0.#");
            }
        }
        return "";
    }

    // Retter brugerens opgave og tilpasser parametre, som justerer
    // på sværhedsgraden i opgaverne.
    public string Correct(string function)
    {
        if (IsUserCorrect(function))
        {
            correctAnswers++;
            correctAnswersInARow++;
            wrongAnswersInARow = 0;
            return "Rigtigt svar. Godt gået kammerat!";
        }
        else
        {
            correctAnswersInARow = 0;
            wrongAnswersInARow++;
            return "Det er desværre forkert. Det rigtige svar er: " + correctAnswer + ".";
        }
    }

    // Tjekker, om brugerens svar er korrekt
    // string function er brugerens svar
    private bool IsUserCorrect(string function) { return function == correctAnswer; }

    // Bestemmer indekset i funktionsforskriften, hvor den uafhængige variabel indtræder
    // string function er den givne funktionsforskrift
    // char var er den uafhængige variabel
    private void SearchForChar(string function, char var)
    {
        // Først ryddes klassevariablen posOfVar's indekser i memory
        System.Array.Clear(posOfVar, 0, posOfVar.Length);

        // Angiver antal gange, man har fundet den uafhængige variabel
        int count = 0;

        // Søg efter bogstavet 'x' i funktionsforskriften og gem i posOfVar-array
        for (int i = 0; i < function.Length; i++)
        {
            if (function[i] == var)
            {
                posOfVar[count] = i;
                count++;
            }
        }
        // Sætter klassevariablen orderOfFunc lig antallet gange, variablen blev fundet
        orderOfFunc = count - 1;
    }

    // Kan trække leddene ud af et givent funktionsudtryk
    // string function er funktionsudtrykket
    // char constant er leddet, der skal hives ud, fx 'a', 'b', eller 'c'
    private int GetConstantOfFunction(string function, char constant)
    {
        // Gør noget forskelligt af, hvilket led, man leder efter
        switch (constant)
        {
            case 'a':
                // Fortegnet i funktionsudtrykket
                string sign = "";
                // a-leddet i funktionsudtrkkey
                string constantA = "";

                // Tjekker om fortegnet er et '-'
                if(function[0] == '-')
                {
                    sign = function[0].ToString();
                    // Loop igennem leddet og læg det oven i a-leddet
                    for (int i = 1; i < posOfVar[0]; i++)
                        constantA += function[i];
                }
                else
                {
                    // Loop igennem leddet og læg det oven i a-leddet
                    for (int i = 0; i < posOfVar[0]; i++)
                        constantA += function[i];
                }
                return int.Parse(sign + constantA);
            case 'b':
                // b- og c-leddene findes på en lidt nemmere måde
                return ReturnConstant(function, 1);
            case 'c':
                return ReturnConstant(function, 2);
        }
        return -10300;
    }

    // Kan returnere 'b'- og 'c'-leddene i funktionsudtrykket
    // string function angiver funktionsudtrykket
    // int j angiver positionen, som leddet har i funktionsudtrykket
    private int ReturnConstant(string function, int j)
    {
        // Fortegnet i funktionsudtrykket
        string sign = function[posOfVar[j - 1] + 3].ToString();
        // Leddet, man søger
        string constant = "";

        // Loop igennem og læg tallene fundet til leddet
        for (int i = posOfVar[j - 1]; i < posOfVar[j] - 4; i++)
            constant += function[i + 4];

        return int.Parse(sign + constant);
    }

    // Differentierer en given funktion af brugeren. Virker kun på polynomier
    // string function er den givne funktion, der skal differentieres
    private string Differentiate(string function)
    {
        // Bestem indekset af, hvor den uafhængige variabel er i funktionen
        SearchForChar(function, 'x');
        // Den afledte funktion
        string answer = "";
        // Loop igennem funktionens orden led for led
        for (int i = 0; i < orderOfFunc; i++)
        {
            // Det nuværende led
            string led = "";

            // Bestem det nuværende led
            if (i == 0)
                led = GetConstantOfFunction(function, 'a').ToString();
            else
                led = ReturnConstant(function, i).ToString();

            // Formel: f(x)=a*x^n => f'(x)=n*a*x^(n-1)
            // Bestem 'n' i formlen
            int gammelEksponent = int.Parse(function[posOfVar[i] + 2].ToString());
            // Bestem 'a' i formlen
            int gammelLed = int.Parse(led);
            // bestem 'n - 1' i formlen
            int nyEksponent = gammelEksponent - 1;

            // Benyt formlen
            answer += (gammelEksponent * gammelLed).ToString() + "x^" + nyEksponent.ToString();

            // Sæt '+' eller '-' i funktionsforskrift
            if (i != orderOfFunc - 1 && function[posOfVar[i] + 3] != '-')
                answer += function[posOfVar[i] + 3];
        }
        return answer;
    }
    #endregion
} // Slutningen af Assignment-klassen