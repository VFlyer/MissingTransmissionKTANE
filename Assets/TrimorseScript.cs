using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrimorseScript : MonoBehaviour {
	private static readonly string[] _wordList = new string[] { "ABLE", "ACHE", "ACID", "ACNE", "ACRE", "AGED", "AIDE", "AKIN", "ALSO", "AMID", "APEX", "ARCH", "ARID", "ARMY", "ATOM", "AUNT", "AXES", "AXIS", "AXLE", "BACK", "BAIL", "BAIT", "BAKE", "BALD", "BAND", "BANG", "BANK", "BARE", "BARK", "BARN", "BASE", "BATH", "BATS", "BEAD", "BEAM", "BEAN", "BEAR", "BEAT", "BELT", "BEND", "BENT", "BEST", "BIAS", "BIKE", "BILE", "BIND", "BIRD", "BITE", "BLAH", "BLEW", "BLOW", "BLUE", "BLUR", "BOAR", "BOAT", "BODY", "BOIL", "BOLD", "BOLT", "BOND", "BONE", "BONY", "BORE", "BORN", "BOTH", "BOUT", "BOWL", "BROW", "BULK", "BUMP", "BURN", "BURY", "BUSH", "BUST", "BUSY", "CAFE", "CAGE", "CAKE", "CALF", "CALM", "CAME", "CAMP", "CANE", "CAPE", "CARD", "CARE", "CARP", "CART", "CASE", "CASH", "CAST", "CAVE", "CHAP", "CHAT", "CHEF", "CHIN", "CHIP", "CHOP", "CITY", "CLAD", "CLAM", "CLAN", "CLAP", "CLAW", "CLAY", "CLIP", "CLOG", "CLUB", "CLUE", "COAL", "COAT", "CODE", "COIL", "COIN", "COLD", "COMB", "COME", "CONE", "COPE", "COPY", "CORD", "CORE", "CORK", "CORN", "COST", "COSY", "COUP", "COZY", "CRAB", "CREW", "CRIB", "CROP", "CROW", "CUBE", "CULT", "CURB", "CURE", "CURL", "CUTE", "DAFT", "DAMP", "DARE", "DARK", "DART", "DASH", "DATE", "DAWN", "DAYS", "DEAF", "DEAL", "DEAR", "DEBT", "DECK", "DENT", "DENY", "DESK", "DIAL", "DICE", "DIET", "DINE", "DIRE", "DIRT", "DISC", "DISH", "DISK", "DIVE", "DOCK", "DOLE", "DOME", "DONE", "DOSE", "DOVE", "DOWN", "DRAG", "DRAW", "DREW", "DRIP", "DROP", "DRUG", "DRUM", "DUAL", "DUCK", "DUEL", "DUET", "DULY", "DUMB", "DUMP", "DUSK", "DUST", "DUTY", "EACH", "EARN", "EARS", "EAST", "EASY", "EATS", "ECHO", "EDIT", "ENVY", "EPIC", "EURO", "EVIL", "EXAM", "EXIT", "FACE", "FACT", "FADE", "FAIL", "FAIR", "FAKE", "FAME", "FARE", "FARM", "FAST", "FATE", "FEAR", "FEAT", "FELT", "FILE", "FILM", "FIND", "FINE", "FIRE", "FIRM", "FISH", "FIST", "FIVE", "FLAG", "FLAP", "FLAT", "FLAW", "FLED", "FLEW", "FLEX", "FLIP", "FLOW", "FLUX", "FOAM", "FOIL", "FOLD", "FOLK", "FOND", "FONT", "FORD", "FORK", "FORM", "FORT", "FOUL", "FOUR", "FROG", "FROM", "FUEL", "FUND", "FURY", "FUSE", "GAIN", "GAME", "GASP", "GATE", "GAVE", "GAZE", "GEAR", "GERM", "GIFT", "GILT", "GIRL", "GIVE", "GLAD", "GLOW", "GLUE", "GOAL", "GOAT", "GOES", "GOLD", "GOLF", "GONE", "GOSH", "GOWN", "GRAB", "GRAM", "GRAY", "GREW", "GREY", "GRID", "GRIM", "GRIN", "GRIP", "GRIT", "GROW", "GUST", "HAIL", "HAIR", "HALF", "HALT", "HAND", "HANG", "HARD", "HARE", "HARM", "HATE", "HAUL", "HAVE", "HAWK", "HAZE", "HEAD", "HEAL", "HEAP", "HEAR", "HEAT", "HEIR", "HELD", "HELP", "HERB", "HERD", "HERO", "HERS", "HIDE", "HIKE", "HINT", "HIRE", "HOLD", "HOLE", "HOLY", "HOME", "HOPE", "HORN", "HOSE", "HOST", "HOUR", "HOWL", "HUGE", "HUNG", "HUNT", "HURT", "HYMN", "HYPE", "ICED", "ICON", "IDEA", "IDLE", "IDOL", "INCH", "INFO", "INTO", "IRON", "ITCH", "ITEM", "JACK", "JAIL", "JARS", "JINX", "JOBS", "JOIN", "JOKE", "JUMP", "JUNK", "JURY", "JUST", "KELP", "KEPT", "KIND", "KING", "KITE", "KNEW", "KNIT", "KNOB", "KNOT", "KNOW", "LACE", "LACK", "LADY", "LAID", "LAIR", "LAKE", "LAMB", "LAMP", "LAND", "LANE", "LAST", "LATE", "LAWN", "LAZY", "LEAD", "LEAF", "LEAK", "LEAN", "LEAP", "LEFT", "LEND", "LENS", "LEST", "LEVY", "LIAR", "LIED", "LIFE", "LIFT", "LIKE", "LIMB", "LIME", "LINE", "LINK", "LION", "LIST", "LIVE", "LOAD", "LOAF", "LOAN", "LOCK", "LOFT", "LONE", "LONG", "LORD", "LOSE", "LOST", "LOTS", "LOUD", "LOVE", "LUCK", "LUMP", "LUNG", "LURE", "LUSH", "LUST", "MADE", "MAID", "MAIL", "MAIN", "MAKE", "MALE", "MALT", "MANY", "MARE", "MARK", "MASK", "MAST", "MATE", "MATH", "MAZE", "MEAL", "MEAN", "MEAT", "MELT", "MENU", "MESH", "MICE", "MILD", "MILE", "MILK", "MIND", "MINE", "MINT", "MIST", "MOAT", "MOCK", "MODE", "MOLD", "MOLE", "MONK", "MORE", "MOST", "MOTH", "MOVE", "MUCH", "MULE", "MUST", "MUTE", "MYTH", "NAIL", "NAME", "NAVE", "NEAR", "NEAT", "NECK", "NEST", "NEWS", "NEWT", "NEXT", "NICE", "NICK", "NODE", "NORM", "NOSE", "NOTE", "NUMB", "NUTS", "OATH", "OATS", "OBEY", "OGRE", "OILY", "OINK", "OKAY", "OMEN", "OMIT", "ONCE", "ONLY", "OPEN", "ORAL", "ORCA", "ORES", "OURS", "OVAL", "OVEN", "OVER", "PACE", "PACK", "PACT", "PAGE", "PAID", "PAIN", "PAIR", "PALE", "PALM", "PARK", "PART", "PAST", "PATH", "PAWN", "PEAK", "PEAR", "PEAT", "PERK", "PEST", "PICK", "PIER", "PILE", "PINE", "PINK", "PINT", "PITY", "PLAN", "PLAY", "PLEA", "PLOT", "PLOW", "PLOY", "PLUG", "PLUM", "PLUS", "POEM", "POET", "POKE", "POLE", "POND", "PONY", "PORK", "PORT", "POSE", "POSH", "POST", "POUR", "PRAY", "PREY", "PUNK", "PURE", "PUSH", "QUAY", "QUID", "QUIT", "QUIZ", "RACE", "RACK", "RAFT", "RAGE", "RAID", "RAIL", "RAIN", "RAKE", "RAMP", "RANK", "RASH", "RATE", "READ", "REAL", "RELY", "RENT", "REST", "RICE", "RICH", "RIDE", "RIFT", "RING", "RIOT", "RIPE", "RISE", "RISK", "RITE", "ROAD", "ROAM", "ROBE", "ROCK", "RODE", "ROLE", "ROPE", "ROSE", "ROSY", "RUBY", "RUDE", "RUIN", "RULE", "RUNG", "RUSH", "RUST", "SACK", "SAFE", "SAID", "SAIL", "SAKE", "SALE", "SALT", "SAME", "SAND", "SANE", "SANG", "SANK", "SAVE", "SCAN", "SCAR", "SCUM", "SEAL", "SEAM", "SEAT", "SELF", "SEND", "SENT", "SEXY", "SHED", "SHIP", "SHOE", "SHOP", "SHOT", "SHOW", "SHUT", "SICK", "SIDE", "SIGH", "SIGN", "SILK", "SING", "SINK", "SITE", "SIZE", "SKIN", "SKIP", "SLAB", "SLAM", "SLID", "SLIM", "SLIP", "SLOT", "SLOW", "SLUM", "SMUG", "SNAP", "SNOW", "SOAP", "SOAR", "SODA", "SOFA", "SOFT", "SOIL", "SOLD", "SOLE", "SOME", "SONG", "SORE", "SORT", "SOUL", "SOUP", "SOUR", "SPAN", "SPIN", "SPOT", "SPUN", "SPUR", "STAB", "STAR", "STAY", "STEM", "STEP", "STIR", "STOP", "SUCH", "SUIT", "SUNG", "SUNK", "SURE", "SWAN", "SWAP", "SWIM", "TACK", "TACO", "TAIL", "TAKE", "TALE", "TALK", "TAME", "TANK", "TAPE", "TASK", "TAXI", "TEAL", "TEAM", "TEAR", "TEND", "TERM", "THAN", "THAW", "THEM", "THEN", "THEY", "THIN", "THIS", "THOU", "THUD", "THUS", "TIDE", "TIDY", "TIED", "TIER", "TILE", "TIME", "TINY", "TIRE", "TOAD", "TOIL", "TOLD", "TOMB", "TONE", "TORE", "TORN", "TORY", "TOUR", "TOWN", "TRAM", "TRAP", "TRAY", "TRIM", "TRIO", "TRIP", "TRUE", "TSAR", "TUBE", "TUCK", "TUNA", "TUNE", "TURF", "TURN", "TWIN", "TYPE", "UGLY", "UNDO", "UNIT", "UNTO", "UPON", "URGE", "USED", "USER", "VAIN", "VARY", "VASE", "VAST", "VEIL", "VEIN", "VENT", "VERB", "VERY", "VEST", "VETO", "VIAL", "VIEW", "VILE", "VINE", "VISA", "VOID", "VOTE", "WADE", "WAGE", "WAIT", "WAKE", "WALK", "WAND", "WANT", "WARD", "WARM", "WARN", "WARP", "WARY", "WASH", "WAVE", "WAVY", "WAXY", "WEAK", "WEAR", "WELD", "WENT", "WEST", "WHAT", "WHEN", "WHIP", "WHOM", "WIDE", "WIFE", "WILD", "WIND", "WINE", "WING", "WIPE", "WIRE", "WISE", "WISH", "WITH", "WOLF", "WOMB", "WORD", "WORE", "WORK", "WORM", "WORN", "WRAP", "WRIT", "YARD", "YARN", "YAWN", "YEAH", "YEAR", "YOGA", "YOUR", "ZEAL", "ZERO", "ZINC", "ZONE" };
	static readonly IEnumerable<int> controlIdxSelection = Enumerable.Range(0, 4);
	static readonly string[] positionalNums = { "1st", "2nd", "3rd", "4th" };
	readonly static Dictionary<char, string> morseValues = new Dictionary<char, string> {
		{ 'A', ".-" },
		{ 'B', "-..." },
		{ 'C', "-.-." },
		{ 'D', "-.." },
		{ 'E', "." },
		{ 'F', "..-." },
		{ 'G', "--." },
		{ 'H', "...." },
		{ 'I', ".." },
		{ 'J', ".---" },
		{ 'K', "-.-" },
		{ 'L', ".-.." },
		{ 'M', "--" },
		{ 'N', "-." },
		{ 'O', "---" },
		{ 'P', ".--." },
		{ 'Q', "--.-" },
		{ 'R', ".-." },
		{ 'S', "..." },
		{ 'T', "-" },
		{ 'U', "..-" },
		{ 'V', "...-" },
		{ 'W', ".--" },
		{ 'X', "-..-" },
		{ 'Y', "-.--" },
		{ 'Z', "--.." },
	};

	public KMBombModule modSelf;
	public KMSelectable[] buttonsSelectable;
	public KMSelectable playSelectable;
	public KMAudio mAudio;
	public AudioSource[] Beeps;
	public TextMesh[] btnText;
	public MeshRenderer progressRenderer;
	public MeshRenderer[] smallLEDs, largeLEDs, buttonBackRenders;
	public Material[] VisualAudioMats;
	public Material[] MorseLightMats;
	public Material[] ButtonMats;



	IEnumerable<int> selectedOrder, idxSoundMorse, idxMixupMorseSounds;
	List<IEnumerable<int>> allValidCombinations;
	List<int> currentOrder;
	
	string selectedWord;
	int idxFaulty = -1, attemptsLeft = -1, numMorsePlaying = 0, idxHoldWhileLocked = -1, maxAttempts = 2;
	float timeHeldPlay = 0f;
	List<IEnumerable<int>> allCombinations;
	bool lockButtons = false, holdingPlay = false;

	static int modIDCnt;
	int moduleID;
	void QuickLog(string toLog, params object[] args)
    {
		Debug.LogFormat("[{0} #{1}] {2}", modSelf.ModuleDisplayName, moduleID, string.Format(toLog, args));
	}
	// Use this for initialization
	void Start () {
		moduleID = ++modIDCnt;
		var storedCombinations = new List<IEnumerable<int>> { Enumerable.Empty<int>() };
        for (var x = 0; x < 4; x++)
        {
			var newCombinations = new List<IEnumerable<int>>();
			foreach (var storedCombo in storedCombinations)
            {
				var allowedValues = controlIdxSelection.Where(a => !storedCombo.Contains(a));
				foreach (var aValue in allowedValues)
				{
					var nextCombo = storedCombo.Concat(new[] { aValue });
					newCombinations.Add(nextCombo);
				}
			}
			if (!newCombinations.Any()) break;
			storedCombinations = newCombinations;
        }

		allCombinations = storedCombinations;
        //Debug.LogFormat("[{0}]",allCombinations.Select(a => a.Join(",")).Join("],["));
        //QuickLog("Generated {0} combinations of buttons to press.", allCombinations.Count);
        idxFaulty = Random.Range(0, controlIdxSelection.Count());
		idxMixupMorseSounds = controlIdxSelection.ToArray().Shuffle();
		QuickLog("The {0} button will not emit or play a sound in Morse at all.", positionalNums[idxFaulty]);
		QuickLog("Press the play button to start the module. Be sure to listen to each button to denote which button plays which pitch.");

        for (var x = 0; x < buttonsSelectable.Length; x++)
        {
			var y = x;
			buttonsSelectable[x].OnInteract += delegate {
				if (lockButtons) {
					idxHoldWhileLocked = y;
					return false;
				}
				if (attemptsLeft < 0 && idxFaulty != y)
                {
					Beeps[idxMixupMorseSounds.ElementAt(y)].Play();
                }
				buttonsSelectable[y].AddInteractionPunch(0.2f);
				return false;
			};
			buttonsSelectable[x].OnInteractEnded += delegate {
				mAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, buttonsSelectable[y].transform);
				if (attemptsLeft < 0)
				{
					Beeps[idxMixupMorseSounds.ElementAt(y)].Stop();
				}
				else if (idxHoldWhileLocked != y)
                {
					ProcessInput(y);
                }
				idxHoldWhileLocked = -1;
			};
        }
		for (var x = 0; x < btnText.Length; x++)
        {
			btnText[x].text = "";
        }

		playSelectable.OnInteract += delegate {
			holdingPlay = true;
			mAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, playSelectable.transform);
			playSelectable.AddInteractionPunch(0.2f);
			idxHoldWhileLocked = lockButtons ? -2 : -1;
			timeHeldPlay = 0f;
			return false;
		};
		playSelectable.OnInteractEnded += delegate {
			holdingPlay = false;
			if (lockButtons || idxHoldWhileLocked == -2) {
				idxHoldWhileLocked = -1;
				return;
			}
			if (timeHeldPlay >= 1f && attemptsLeft >= 0)
			{
				QuickLog("You chose to forget this instance and revert this to the initial mode.");
				attemptsLeft = -1;
				StartCoroutine(PlaySoundSequence());
			}
			else
			{
				attemptsLeft -= 1;
				if (attemptsLeft < 0)
					GenerateNewInstance();
				else
				{
					if (attemptsLeft < 1)
                    {
						idxSoundMorse = controlIdxSelection.ToArray().Shuffle().Take(2);
						QuickLog("These buttons will instead be playing sound in Morse: {0}", idxSoundMorse.Select(a => a + 1).Join(", "));
					}
					currentOrder.Clear();
					StartCoroutine(HandleStartEmission());
				}
			}
			idxHoldWhileLocked = -1;
		};
	}
	void ProcessInput(int idx)
    {
		if (currentOrder.Contains(idx)) return;
		currentOrder.Add(idx);
		mAudio.PlaySoundAtTransform("Scroll" + currentOrder.Count, transform);
		if (currentOrder.Count == 4)
        {
			QuickLog("Pressed the buttons in this order: {0}", currentOrder.Select(a => a + 1).Join(", "));
			QuickLog("Forming the combination: {0}", currentOrder.Select(a => a == idxFaulty ? '?' : selectedWord[selectedOrder.IndexOf(b => a == b)]).Join(""));
			StartCoroutine(CheckSolution());
        }
	}
	bool IsSolutionValid(IEnumerable<int> curCombo)
    {
		var regexString = string.Format(@"^{0}$", curCombo.Select(a => a == idxFaulty ? "[A-Z]" : selectedWord.Substring(a, 1)).Join(""));

		//Debug.Log(curCombo.Select(a => a == idxFaulty ? "?" : selectedWord.Substring(a, 1)).Join(""));
		//Debug.Log(_wordList.Count(a => Regex.IsMatch(a, regexString, RegexOptions.IgnoreCase)));
		return _wordList.Any(a => Regex.IsMatch(a, regexString, RegexOptions.IgnoreCase));
    }
	IEnumerator HandleTimerModification()
    {
		var duration = maxAttempts;
		var elapsed = 0f;
		while (elapsed < duration)
		{
			progressRenderer.transform.localScale = new Vector3(Mathf.Lerp(0.1425f, 0f, elapsed / duration), 0.009f, 1f);
			progressRenderer.transform.localPosition = new Vector3(Mathf.Lerp(0f, -0.07125f, elapsed / duration), 0.0152f, 0.07f);
			yield return null;
			if (elapsed < maxAttempts - attemptsLeft)
				elapsed += Time.deltaTime;
			if (attemptsLeft < 0)
				elapsed = duration;
		}
		progressRenderer.transform.localScale = new Vector3(0f, 0.009f, 1f);
		progressRenderer.transform.localPosition = new Vector3(-0.07125f, 0.0152f, 0.07f);
	}
	void GenerateNewInstance()
    {
		if (currentOrder == null)
			currentOrder = new List<int>();
		else
			currentOrder.Clear();
		var iterationCount = 0;
		var ambiguityPercent = 20;
		do
		{
			selectedWord = _wordList.PickRandom();
			selectedOrder = allCombinations.PickRandom();
			var baseValidCombinations = allCombinations.Where(a => IsSolutionValid(a));
			allValidCombinations = baseValidCombinations.Select(a => a.Select(b => selectedOrder.ElementAt(b))).ToList();
			iterationCount++;
			if (allValidCombinations.Count * 100 <= allCombinations.Count * ambiguityPercent)
				break;
		}
		while (iterationCount < 5);
		if (allValidCombinations.Count * 100 > allCombinations.Count * ambiguityPercent)
			QuickLog("Generated an instance where more than {2}% of all possible combinations can be accepted after {0} iteration{1}.", iterationCount, iterationCount == 1 ? "" : "s", ambiguityPercent);
		QuickLog("Selected word: {0}", selectedWord);
		QuickLog("Selected combination of buttons emitting that word (from left to right): {0}", selectedOrder.Select(a => a + 1).Join(", "));
		QuickLog("All acceptable button combinations to press: [{0}]", allValidCombinations.Select(a => a.Select(b => b + 1).Join(",")).Join("];["));
		idxSoundMorse = controlIdxSelection.ToArray().Shuffle().Take(2);
		QuickLog("These buttons will be playing sound in Morse: {0}", idxSoundMorse.Select(a => a + 1).Join(", "));
		StartCoroutine(HandleStartEmission());
		attemptsLeft = maxAttempts;
		StartCoroutine(HandleTimerModification());
	}
	IEnumerator PlaySoundSequence()
    {
		for (var x = 0; x < 4; x++)
		{
			mAudio.PlaySoundAtTransform(string.Format("Scroll{0}", 4 - x), transform);
			yield return new WaitForSeconds(0.05f);
		}
	}
	IEnumerator SolveModule()
    {
		mAudio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.CorrectChime, transform);
		modSelf.HandlePass();
		for (var x = 0; x < selectedOrder.Count(); x++)
		{
			btnText[selectedOrder.ElementAt(x)].text = selectedOrder.ElementAt(x) == idxFaulty ? "?" : selectedWord.Substring(x, 1);
			yield return new WaitForSeconds(0.3f);
		}
	}
	IEnumerator CheckSolution()
    {
		foreach (var btnMesh in buttonBackRenders)
			btnMesh.material = ButtonMats[1];
		lockButtons = true;
		yield return new WaitForSeconds(0.25f);
		for (var x = 0; x < currentOrder.Count; x++)
        {
			btnText[currentOrder[x]].text = (x + 1).ToString();
			mAudio.PlaySoundAtTransform(string.Format("Scroll{0}",x + 1), transform);
			yield return new WaitForSeconds(0.25f);
        }
		if (allValidCombinations.Any(a => currentOrder.SequenceEqual(a)) || IsSolutionValid(currentOrder.Select(b => selectedOrder.ElementAt(b))))
		{
			QuickLog("This combination is a valid combination. Module disarmed.");
			yield return SolveModule();
			yield break;
		}
		QuickLog("That is not a valid combination. And the module was unable to find any other words with that combination. Strike! Reverting back to intial state.");
		modSelf.HandleStrike();
		currentOrder.Clear();
		foreach (var btnMesh in buttonBackRenders)
			btnMesh.material = ButtonMats[0];
		foreach (var txt in btnText)
			txt.text = "";
		lockButtons = false;
		attemptsLeft = -1;
		yield return null;
	}
	IEnumerator HandleStartEmission()
    {
		foreach (var btnMesh in buttonBackRenders)
			btnMesh.material = ButtonMats[1];
		lockButtons = true;
        for (int i = 0; i < smallLEDs.Length; i++)
			smallLEDs[i].material = VisualAudioMats[idxSoundMorse.Contains(i) ? 2 : 1];
		foreach (var remainingIdx in controlIdxSelection.Where(a => a != idxFaulty))
		{
			var idxCurOrder = selectedOrder.IndexOf(a => a == remainingIdx);
			StartCoroutine(PlayMorse(remainingIdx, selectedWord[idxCurOrder], idxSoundMorse.Contains(remainingIdx)));
		}
		do
			yield return null;
		while (numMorsePlaying > 0);
		for (int i = 0; i < smallLEDs.Length; i++)
			smallLEDs[i].material = VisualAudioMats[0];
		foreach (var btnMesh in buttonBackRenders)
			btnMesh.material = ButtonMats[0];
		lockButtons = false;
		yield break;
	}

	IEnumerator PlayMorse(int idx, char givenLetter, bool isAuditory = false)
    {
		numMorsePlaying++;
		yield return new WaitForSeconds(0.25f);
		var providedMorse = morseValues.ContainsKey(givenLetter) ? morseValues[givenLetter] : "-----";
        for (var x = 0; x < providedMorse.Length; x++)
        {
			if (isAuditory)
				Beeps[idxMixupMorseSounds.ElementAt(idx)].Play();
			else
				largeLEDs[idx].material = MorseLightMats[1];
			yield return new WaitForSeconds(providedMorse[x] == '.' ? 0.25f : 0.75f);
			if (isAuditory)
				Beeps[idxMixupMorseSounds.ElementAt(idx)].Stop();
			else
				largeLEDs[idx].material = MorseLightMats[0];
			yield return new WaitForSeconds(0.25f);
		}
		numMorsePlaying--;
		yield break;
    }

	// Update is called once per frame
	void Update () {
		if (holdingPlay)
			timeHeldPlay += Time.deltaTime;
	}
#pragma warning disable 0414
	private readonly string TwitchHelpMessage = "Press the play button with !{0} play.  Press the four buttons with !{0} press 1 2 3 4.";
#pragma warning restore 0414
	IEnumerator ProcessTwitchCommand(string cmd)
    {
		yield break;
    }
}
