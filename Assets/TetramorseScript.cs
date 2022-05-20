using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class TetramorseScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;

    public KMSelectable[] CircleButtons;
    public KMSelectable PlayButton;
    public GameObject[] ScreenObjs;
    public GameObject[] SmallObjs;
    public GameObject[] ButtonObjs;
    public AudioSource[] Beeps;
    public Material[] VisualAudioMats;
    public Material[] MorseLightMats;
    public Material[] ButtonMats;
    public TextMesh[] ButtonTexts;
    public GameObject TimerBar;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;

    private int[] _currentLetters = new int[4];
    private bool[] _isVisual = new bool[4];
    private bool[] _hasPressed = new bool[4];
    private bool[] _isPlayingMorse = new bool[4];
    private bool _canPress;
    private int[] _solution = new int[4];
    private List<int> _currentInput = new List<int>();
    private Coroutine[] _showMorse = new Coroutine[4];
    private static readonly string[] _morseCode = new string[26] { ".-", "-...", "-.-.", "-..", ".", "..-.", "--.", "....", "..", ".---", "-.-", ".-..", "--", "-.", "---", ".--.", "--.-", ".-.", "...", "-", "..-", "...-", ".--", "-..-", "-.--", "--.." };
    private static readonly string[] _wordList = new string[] { "ABLE", "ACHE", "ACID", "ACNE", "ACRE", "AGED", "AIDE", "AKIN", "ALSO", "AMID", "APEX", "ARCH", "ARID", "ARMY", "ATOM", "AUNT", "AXES", "AXIS", "AXLE", "BACK", "BAIL", "BAIT", "BAKE", "BALD", "BAND", "BANG", "BANK", "BARE", "BARK", "BARN", "BASE", "BATH", "BATS", "BEAD", "BEAM", "BEAN", "BEAR", "BEAT", "BELT", "BEND", "BENT", "BEST", "BIAS", "BIKE", "BILE", "BIND", "BIRD", "BITE", "BLAH", "BLEW", "BLOW", "BLUE", "BLUR", "BOAR", "BOAT", "BODY", "BOIL", "BOLD", "BOLT", "BOND", "BONE", "BONY", "BORE", "BORN", "BOTH", "BOUT", "BOWL", "BROW", "BULK", "BUMP", "BURN", "BURY", "BUSH", "BUST", "BUSY", "CAFE", "CAGE", "CAKE", "CALF", "CALM", "CAME", "CAMP", "CANE", "CAPE", "CARD", "CARE", "CARP", "CART", "CASE", "CASH", "CAST", "CAVE", "CHAP", "CHAT", "CHEF", "CHIN", "CHIP", "CHOP", "CITY", "CLAD", "CLAM", "CLAN", "CLAP", "CLAW", "CLAY", "CLIP", "CLOG", "CLUB", "CLUE", "COAL", "COAT", "CODE", "COIL", "COIN", "COLD", "COMB", "COME", "CONE", "COPE", "COPY", "CORD", "CORE", "CORK", "CORN", "COST", "COSY", "COUP", "COZY", "CRAB", "CREW", "CRIB", "CROP", "CROW", "CUBE", "CULT", "CURB", "CURE", "CURL", "CUTE", "DAFT", "DAMP", "DARE", "DARK", "DART", "DASH", "DATE", "DAWN", "DAYS", "DEAF", "DEAL", "DEAR", "DEBT", "DECK", "DENT", "DENY", "DESK", "DIAL", "DICE", "DIET", "DINE", "DIRE", "DIRT", "DISC", "DISH", "DISK", "DIVE", "DOCK", "DOLE", "DOME", "DONE", "DOSE", "DOVE", "DOWN", "DRAG", "DRAW", "DREW", "DRIP", "DROP", "DRUG", "DRUM", "DUAL", "DUCK", "DUEL", "DUET", "DULY", "DUMB", "DUMP", "DUSK", "DUST", "DUTY", "EACH", "EARN", "EARS", "EAST", "EASY", "EATS", "ECHO", "EDIT", "ENVY", "EPIC", "EURO", "EVIL", "EXAM", "EXIT", "FACE", "FACT", "FADE", "FAIL", "FAIR", "FAKE", "FAME", "FARE", "FARM", "FAST", "FATE", "FEAR", "FEAT", "FELT", "FILE", "FILM", "FIND", "FINE", "FIRE", "FIRM", "FISH", "FIST", "FIVE", "FLAG", "FLAP", "FLAT", "FLAW", "FLED", "FLEW", "FLEX", "FLIP", "FLOW", "FLUX", "FOAM", "FOIL", "FOLD", "FOLK", "FOND", "FONT", "FORD", "FORK", "FORM", "FORT", "FOUL", "FOUR", "FROG", "FROM", "FUEL", "FUND", "FURY", "FUSE", "GAIN", "GAME", "GASP", "GATE", "GAVE", "GAZE", "GEAR", "GERM", "GIFT", "GILT", "GIRL", "GIVE", "GLAD", "GLOW", "GLUE", "GOAL", "GOAT", "GOES", "GOLD", "GOLF", "GONE", "GOSH", "GOWN", "GRAB", "GRAM", "GRAY", "GREW", "GREY", "GRID", "GRIM", "GRIN", "GRIP", "GRIT", "GROW", "GUST", "HAIL", "HAIR", "HALF", "HALT", "HAND", "HANG", "HARD", "HARE", "HARM", "HATE", "HAUL", "HAVE", "HAWK", "HAZE", "HEAD", "HEAL", "HEAP", "HEAR", "HEAT", "HEIR", "HELD", "HELP", "HERB", "HERD", "HERO", "HERS", "HIDE", "HIKE", "HINT", "HIRE", "HOLD", "HOLE", "HOLY", "HOME", "HOPE", "HORN", "HOSE", "HOST", "HOUR", "HOWL", "HUGE", "HUNG", "HUNT", "HURT", "HYMN", "HYPE", "ICED", "ICON", "IDEA", "IDLE", "IDOL", "INCH", "INFO", "INTO", "IRON", "ITCH", "ITEM", "JACK", "JAIL", "JARS", "JINX", "JOBS", "JOIN", "JOKE", "JUMP", "JUNK", "JURY", "JUST", "KELP", "KEPT", "KIND", "KING", "KITE", "KNEW", "KNIT", "KNOB", "KNOT", "KNOW", "LACE", "LACK", "LADY", "LAID", "LAIR", "LAKE", "LAMB", "LAMP", "LAND", "LANE", "LAST", "LATE", "LAWN", "LAZY", "LEAD", "LEAF", "LEAK", "LEAN", "LEAP", "LEFT", "LEND", "LENS", "LEST", "LEVY", "LIAR", "LIED", "LIFE", "LIFT", "LIKE", "LIMB", "LIME", "LINE", "LINK", "LION", "LIST", "LIVE", "LOAD", "LOAF", "LOAN", "LOCK", "LOFT", "LONE", "LONG", "LORD", "LOSE", "LOST", "LOTS", "LOUD", "LOVE", "LUCK", "LUMP", "LUNG", "LURE", "LUSH", "LUST", "MADE", "MAID", "MAIL", "MAIN", "MAKE", "MALE", "MALT", "MANY", "MARE", "MARK", "MASK", "MAST", "MATE", "MATH", "MAZE", "MEAL", "MEAN", "MEAT", "MELT", "MENU", "MESH", "MICE", "MILD", "MILE", "MILK", "MIND", "MINE", "MINT", "MIST", "MOAT", "MOCK", "MODE", "MOLD", "MOLE", "MONK", "MORE", "MOST", "MOTH", "MOVE", "MUCH", "MULE", "MUST", "MUTE", "MYTH", "NAIL", "NAME", "NAVE", "NEAR", "NEAT", "NECK", "NEST", "NEWS", "NEWT", "NEXT", "NICE", "NICK", "NODE", "NORM", "NOSE", "NOTE", "NUMB", "NUTS", "OATH", "OATS", "OBEY", "OGRE", "OILY", "OINK", "OKAY", "OMEN", "OMIT", "ONCE", "ONLY", "OPEN", "ORAL", "ORCA", "ORES", "OURS", "OVAL", "OVEN", "OVER", "PACE", "PACK", "PACT", "PAGE", "PAID", "PAIN", "PAIR", "PALE", "PALM", "PARK", "PART", "PAST", "PATH", "PAWN", "PEAK", "PEAR", "PEAT", "PERK", "PEST", "PICK", "PIER", "PILE", "PINE", "PINK", "PINT", "PITY", "PLAN", "PLAY", "PLEA", "PLOT", "PLOW", "PLOY", "PLUG", "PLUM", "PLUS", "POEM", "POET", "POKE", "POLE", "POND", "PONY", "PORK", "PORT", "POSE", "POSH", "POST", "POUR", "PRAY", "PREY", "PUNK", "PURE", "PUSH", "QUAY", "QUID", "QUIT", "QUIZ", "RACE", "RACK", "RAFT", "RAGE", "RAID", "RAIL", "RAIN", "RAKE", "RAMP", "RANK", "RASH", "RATE", "READ", "REAL", "RELY", "RENT", "REST", "RICE", "RICH", "RIDE", "RIFT", "RING", "RIOT", "RIPE", "RISE", "RISK", "RITE", "ROAD", "ROAM", "ROBE", "ROCK", "RODE", "ROLE", "ROPE", "ROSE", "ROSY", "RUBY", "RUDE", "RUIN", "RULE", "RUNG", "RUSH", "RUST", "SACK", "SAFE", "SAID", "SAIL", "SAKE", "SALE", "SALT", "SAME", "SAND", "SANE", "SANG", "SANK", "SAVE", "SCAN", "SCAR", "SCUM", "SEAL", "SEAM", "SEAT", "SELF", "SEND", "SENT", "SEXY", "SHED", "SHIP", "SHOE", "SHOP", "SHOT", "SHOW", "SHUT", "SICK", "SIDE", "SIGH", "SIGN", "SILK", "SING", "SINK", "SITE", "SIZE", "SKIN", "SKIP", "SLAB", "SLAM", "SLID", "SLIM", "SLIP", "SLOT", "SLOW", "SLUM", "SMUG", "SNAP", "SNOW", "SOAP", "SOAR", "SODA", "SOFA", "SOFT", "SOIL", "SOLD", "SOLE", "SOME", "SONG", "SORE", "SORT", "SOUL", "SOUP", "SOUR", "SPAN", "SPIN", "SPOT", "SPUN", "SPUR", "STAB", "STAR", "STAY", "STEM", "STEP", "STIR", "STOP", "SUCH", "SUIT", "SUNG", "SUNK", "SURE", "SWAN", "SWAP", "SWIM", "TACK", "TACO", "TAIL", "TAKE", "TALE", "TALK", "TAME", "TANK", "TAPE", "TASK", "TAXI", "TEAL", "TEAM", "TEAR", "TEND", "TERM", "THAN", "THAW", "THEM", "THEN", "THEY", "THIN", "THIS", "THOU", "THUD", "THUS", "TIDE", "TIDY", "TIED", "TIER", "TILE", "TIME", "TINY", "TIRE", "TOAD", "TOIL", "TOLD", "TOMB", "TONE", "TORE", "TORN", "TORY", "TOUR", "TOWN", "TRAM", "TRAP", "TRAY", "TRIM", "TRIO", "TRIP", "TRUE", "TSAR", "TUBE", "TUCK", "TUNA", "TUNE", "TURF", "TURN", "TWIN", "TYPE", "UGLY", "UNDO", "UNIT", "UNTO", "UPON", "URGE", "USED", "USER", "VAIN", "VARY", "VASE", "VAST", "VEIL", "VEIN", "VENT", "VERB", "VERY", "VEST", "VETO", "VIAL", "VIEW", "VILE", "VINE", "VISA", "VOID", "VOTE", "WADE", "WAGE", "WAIT", "WAKE", "WALK", "WAND", "WANT", "WARD", "WARM", "WARN", "WARP", "WARY", "WASH", "WAVE", "WAVY", "WAXY", "WEAK", "WEAR", "WELD", "WENT", "WEST", "WHAT", "WHEN", "WHIP", "WHOM", "WIDE", "WIFE", "WILD", "WIND", "WINE", "WING", "WIPE", "WIRE", "WISE", "WISH", "WITH", "WOLF", "WOMB", "WORD", "WORE", "WORK", "WORM", "WORN", "WRAP", "WRIT", "YARD", "YARN", "YAWN", "YEAH", "YEAR", "YOGA", "YOUR", "ZEAL", "ZERO", "ZINC", "ZONE" };
    private string _currentWord = "asdf";
    private float _timerLength = 20f;
    private Coroutine _timer;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        PlayButton.OnInteract += PlayPress;
        for (int i = 0; i < CircleButtons.Length; i++)
            CircleButtons[i].OnInteract += CirclePress(i);
        for (int i = 0; i < 4; i++)
            ButtonTexts[i].text = "";
    }

    private bool PlayPress()
    {
        PlayButton.AddInteractionPunch(0.5f);
        if (_isPlayingMorse.Contains(true) || _moduleSolved)
            return false;
        if (_timer != null)
            StopCoroutine(_timer);
        _timer = StartCoroutine(Timer());
        for (int i = 0; i < 4; i++)
        {
            ButtonTexts[i].text = "";
            ButtonObjs[i].GetComponent<MeshRenderer>().material = ButtonMats[0];
        }
        _currentInput = new List<int>();
        _hasPressed = new bool[4];
        _currentWord = _wordList.PickRandom();
        _isVisual = Enumerable.Range(0, 4).ToArray().Shuffle().Select(i => i % 2 == 0 ? true : false).ToArray();
        for (int i = 0; i < 4; i++)
        {
            _currentLetters[i] = _currentWord[i] - 'A';
            SmallObjs[i].GetComponent<MeshRenderer>().material = VisualAudioMats[_isVisual[i] ? 0 : 1];
            _showMorse[i] = StartCoroutine(ShowMorse(i, _currentLetters[i], _isVisual[i]));
        }
        Debug.LogFormat("[Tetramorse #{0}] The word is {1}.", _moduleId, _currentWord);
        var order = _currentLetters.ToArray();
        Array.Sort(order);
        for (int i = 0; i < _solution.Length; i++)
            _solution[i] = Array.IndexOf(_currentLetters, order[i]);
        Debug.LogFormat("[Tetramorse #{0}] The order to press the buttons is: {1}.", _moduleId, _solution.Select(i => i + 1).Join(", "));
        return false;
    }

    private KMSelectable.OnInteractHandler CirclePress(int btn)
    {
        return delegate ()
        {
            CircleButtons[btn].AddInteractionPunch(0.5f);
            if (_moduleSolved || !_canPress || _hasPressed[btn])
                return false;
            _hasPressed[btn] = true;
            _currentInput.Add(btn);
            ButtonTexts[btn].text = _currentInput.Count.ToString();
            Audio.PlaySoundAtTransform("Scroll" + _currentInput.Count, transform);
            if (_currentInput.Count == 4)
                StartCoroutine(CheckAnswer());
            return false;
        };
    }

    private IEnumerator ShowMorse(int pos, int letter, bool visual)
    {
        _canPress = false;
        _isPlayingMorse[pos] = true;
        yield return new WaitForSeconds(0.7f);
        yield return new WaitForSeconds(Rnd.Range(0f, 0.1f));
        var time = Rnd.Range(0.3f, 0.4f);
        var morse = _morseCode[letter];
        for (int i = 0; i < morse.Length; i++)
        {
            if (visual)
                ScreenObjs[pos].GetComponent<MeshRenderer>().material = MorseLightMats[1];
            else
                Beeps[pos].Play();
            yield return new WaitForSeconds(time);
            if (morse[i] == '-')
                yield return new WaitForSeconds(time * 2f);
            if (visual)
                ScreenObjs[pos].GetComponent<MeshRenderer>().material = MorseLightMats[0];
            else
                Beeps[pos].Stop();
            yield return new WaitForSeconds(time);
        }
        _isPlayingMorse[pos] = false;
        while (_isPlayingMorse.Contains(true))
            yield return null;
        SmallObjs[pos].GetComponent<MeshRenderer>().material = MorseLightMats[0];
        _canPress = true;
        for (int i = 0; i < ButtonObjs.Length; i++)
            ButtonObjs[i].GetComponent<MeshRenderer>().material = ButtonMats[1];
        yield break;
    }

    private IEnumerator Timer()
    {
        var duration = _timerLength;
        var elapsed = 0f;
        while (elapsed < duration)
        {
            TimerBar.transform.localScale = new Vector3(Mathf.Lerp(0.1425f, 0f, elapsed / duration), 0.009f, 1f);
            TimerBar.transform.localPosition = new Vector3(Mathf.Lerp(0f, -0.07125f, elapsed / duration), 0.0152f, -0.07f);
            yield return null;
            elapsed += Time.deltaTime;
        }
        TimerBar.transform.localScale = new Vector3(0f, 0.009f, 1f);
        TimerBar.transform.localPosition = new Vector3(-0.07125f, 0.0152f, -0.07f);
        _canPress = false;
        for (int i = 0; i < 4; i++)
        {
            ButtonTexts[i].text = "";
            ButtonObjs[i].GetComponent<MeshRenderer>().material = ButtonMats[0];
        }
        _currentInput = new List<int>();
        _hasPressed = new bool[4];
        for (int i = 4; i > 0; i--)
        {
            Audio.PlaySoundAtTransform("Scroll" + i, transform);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator CheckAnswer()
    {
        Debug.LogFormat("[Tetramorse #{0}] Inputted: {1}.", _moduleId, _currentInput.Select(i => i + 1).Join(", "));
        _canPress = false;
        yield return new WaitForSeconds(0.3f);
        bool correct = true;
        for (int i = 0; i < 4; i++)
            if (_currentInput[i] != _solution[i])
                correct = false;
        for (int i = 0; i < 4; i++)
        {
            Audio.PlaySoundAtTransform("Scroll" + (i + 1), transform);
            ButtonTexts[_currentInput[i]].text = _currentWord[_currentInput[i]].ToString();
            yield return new WaitForSeconds(0.3f);
        }
        if (correct)
        {
            Debug.LogFormat("[Tetramorse #{0}] Module solved.", _moduleId);
            _moduleSolved = true;
            Module.HandlePass();
            if (_timer != null)
                StopCoroutine(_timer);
            TimerBar.transform.localScale = new Vector3(0, 0, 0);
            Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
        }
        else
        {
            Debug.LogFormat("[Tetramorse #{0}] Strike.", _moduleId);
            Module.HandleStrike();
            for (int i = 0; i < 4; i++)
            {
                ButtonTexts[i].text = "";
                ButtonObjs[i].GetComponent<MeshRenderer>().material = ButtonMats[0];
            }
        }
    }

#pragma warning disable 0414
    private readonly string TwitchHelpMessage = "Press the play button with !{0} play. Press the four buttons with !{0} press 1 2 3 4.";
#pragma warning restore 0414

    private IEnumerator ProcessTwitchCommand(string command)
    {
        _timerLength = 40f;
        var m = Regex.Match(command, @"^\s*play\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (m.Success)
        {
            if (_isPlayingMorse.Contains(true))
            {
                yield return "sendtochaterror The Morse code is currently playing!";
            }
            yield return null;
            PlayButton.OnInteract();
            yield break;
        }
        var parameters = command.ToLowerInvariant().Split(' ');
        m = Regex.Match(parameters[0], @"^\s*press\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (!m.Success)
            yield break;
        if (parameters.Length != 5)
            yield break;
        var list = new List<int>();
        for (int i = 1; i < parameters.Length; i++)
        {
            int num;
            if (!int.TryParse(parameters[i], out num) || num < 1 || num > 4)
                yield break;
            list.Add(num);
        }
        if (!_canPress)
        {
            yield return "sendtochaterror You cannot press the buttons yet!";
            yield break;
        }
        if (list.Distinct().Count() != 4)
        {
            yield return "sendtochaterror You must press four different buttons!";
            yield break;
        }
        yield return null;
        yield return "strike";
        yield return "solve";
        for (int i = 0; i < list.Count; i++)
        {
            CircleButtons[list[i]].OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator TwitchHandleForcedSolve()
    {
        if (_solution.Distinct().Count() == 1)
            PlayButton.OnInteract();
        int sofar = 0;
        for (int i = 0; i < _currentInput.Count; i++)
        {
            if (_currentInput[i] == _solution[i])
                sofar++;
            else
            {
                PlayButton.OnInteract();
                i = 4;
            }
        }
        while (!_canPress)
            yield return null;
        for (int i = sofar; i < _solution.Length; i++)
        {
            CircleButtons[_solution[i]].OnInteract();
            yield return new WaitForSeconds(0.1f);
        }
        while (!_moduleSolved)
            yield return true;
    }
}
