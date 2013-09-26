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

        public SecretNumber() {
            _guessedNumbers = new GuessedNumber[MaxNumberOfGuesses];
            Initialize();
        }

        public bool CanMakeGuess {
            get {
                return Outcome != Outcome.NoMoreGuesses && Outcome != Outcome.Right;
            }
            private set {
                if (value) {
                    if (Outcome == Outcome.NoMoreGuesses) {
                        Outcome = Outcome.Indefinite;
                    }
                } else {
                    Outcome = Outcome.NoMoreGuesses;
                    Number = _number;
                }
            }
        }

        public int Count {
            get;
            private set;
        }

        public int? Guess {
            get;
            private set;
        }

        public GuessedNumber[] GuessedNumbers {
            get {
                return (GuessedNumber[]) _guessedNumbers.Clone();
            }
        }

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

        public Outcome Outcome {
            get;
            private set;
        }

        public void Initialize() {
            for (int i = 0; i < _guessedNumbers.Length; i++) {
                _guessedNumbers[i].Number = null;
                _guessedNumbers[i].Outcome = Outcome.Indefinite;
            }

            Random random = new Random();
            Number = random.Next(1, 101);

            Count = 0;
            Guess = null;
            Outcome = Outcome.Indefinite;
        }

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
                CanMakeGuess = false;
                return Outcome;
            }

            Outcome outcome;

            if (Guess < _number) {
                outcome = Outcome.Low;
            } else if (Guess > _number) {
                outcome = Outcome.High;
            } else {
                outcome = Outcome.Right;
                Outcome = outcome;
                return outcome;
            }

            _guessedNumbers[Count].Number = Guess;
            _guessedNumbers[Count].Outcome = Outcome;

            Outcome = outcome;
            Count++;

            return outcome;
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
