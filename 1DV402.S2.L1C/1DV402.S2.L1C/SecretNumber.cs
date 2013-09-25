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

        public bool CanMakeGuess {
            get {
                return Outcome != Outcome.NoMoreGuesses;
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

            if (Count >= 6) {
                Count = 7;
                CanMakeGuess = false;
                return Outcome;
            }

            Outcome outcome;

            if (Guess < _number) {
                outcome = Outcome.Low;
            } else if (Guess > _number) {
                outcome = Outcome.High;
            } else {
                Outcome = Outcome.Right;
                CanMakeGuess = false;
                return Outcome;
            }

            GuessedNumber number = new GuessedNumber();
            number.Number = Guess;
            number.Outcome = Outcome;
            _guessedNumbers[Count] = number;

            Outcome = outcome;
            Count++;

            return outcome;
        }

        public SecretNumber() {
            _guessedNumbers = new GuessedNumber[7];
            Initialize();
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
