using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L1C {
    public class SecretNumber {
        public const int MaxNumberOfGuesses = 7;

        private GuessedNumber[] _guessedNumbers;
        private int? _number;

        /// <summary>
        /// Controlls if a guess can be made or not.
        /// </summary>
        public bool CanMakeGuess {
            get {
                return Outcome != Outcome.NoMoreGuesses && Outcome != Outcome.Right;
            }
        }

        /// <summary>
        /// Holds the number of guesses.
        /// </summary>
        public int Count {
            get;
            private set;
        }

        /// <summary>
        /// Holds the latest guess.
        /// </summary>
        public int? Guess {
            get;
            private set;
        }

        /// <summary>
        /// Holds the guessed numbers, returns a copy so it can't be modified.
        /// </summary>
        public GuessedNumber[] GuessedNumbers {
            get {
                return (GuessedNumber[]) _guessedNumbers.Clone();
            }
        }

        /// <summary>
        /// Holds the secret number, returns null if not allowed to read.
        /// </summary>
        public int? Number {
            get {
                if (CanMakeGuess) {
                    return null;
                }
                return _number;
            }
            private set {
                _number = value;
            }
        }

        /// <summary>
        /// Holds the current outcome.
        /// </summary>
        public Outcome Outcome {
            get;
            private set;
        }

        public SecretNumber() {
            _guessedNumbers = new GuessedNumber[MaxNumberOfGuesses];
            Initialize();
        }

        /// <summary>
        /// Initializes SecretNumber to a clean state.
        /// </summary>
        public void Initialize() {
            for (int i = 0; i < _guessedNumbers.Length; i++) {
                _guessedNumbers[i].Number = null;
                _guessedNumbers[i].Outcome = Outcome.Indefinite;
            }
            //_guessedNumbers.Initialize();

            Random random = new Random();
            Number = random.Next(1, 101);

            Count = 0;
            Guess = null;
            Outcome = Outcome.Indefinite;
        }

        /// <summary>
        /// Make a guess at the secret number and handles all inner game logic.
        /// </summary>
        /// <param name="guess">The number to guess</param>
        /// <returns>The outcome</returns>
        public Outcome MakeGuess(int guess) {
            if (guess > 100 || guess < 1) {
                throw new ArgumentOutOfRangeException("guess is not between 1 and 100");
            }

            Guess = guess;

            foreach (GuessedNumber n in _guessedNumbers) {
                if (n.Number == Guess) {
                    Outcome = Outcome.OldGuess;
                    return Outcome.OldGuess;
                }
            }

            if (Count >= MaxNumberOfGuesses - 1) {
                Count = MaxNumberOfGuesses;
                Outcome = Outcome.NoMoreGuesses;
                return Outcome;
            }

            if (Guess < _number) {
                Outcome = Outcome.Low;
            } else if (Guess > _number) {
                Outcome = Outcome.High;
            } else {
                Outcome = Outcome.Right;
                return Outcome;
            }

            _guessedNumbers[Count].Number = Guess;
            _guessedNumbers[Count].Outcome = Outcome;

            Count++;

            return Outcome;
        }
    }

    public enum Outcome {
        Indefinite,
        Low,
        High,
        Right,
        NoMoreGuesses,
        OldGuess
    }
}
