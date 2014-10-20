using System;
using System.Collections.Generic;

namespace Homework3 {
    internal class IsNumberPrimeCalculator {
        private readonly ICollection<long> _primeNumbers;
        private readonly Queue<long> _numbersToCheck;

        public IsNumberPrimeCalculator(ICollection<long> primeNumbers, Queue<long> numbersToCheck) {
            _primeNumbers = primeNumbers;
            _numbersToCheck = numbersToCheck;
        }

        public void CheckIfNumbersArePrime() {
            while (true) {
                //lock
                long numberToCheck = -1;
                lock (Calculator._lock)
                {
                    if (_numbersToCheck.Count != 0)
                    {
                        //Console.WriteLine(_numbersToCheck.Count);
                        numberToCheck = _numbersToCheck.Dequeue();
                    }
                }
                if (numberToCheck != -1)
                {
                    //unlock
                    if (IsNumberPrime(numberToCheck))
                    {
                        //lock
                        lock (Calculator._lock)
                        {
                            _primeNumbers.Add(numberToCheck);
                        }
                        //unlock
                    }
                }
            }
        }

        private bool IsNumberPrime(long numberWeAreChecking) {
            const long firstNumberToCheck = 3;

            if (numberWeAreChecking % 2 == 0) {
                return false;
            }
            var lastNumberToCheck = Math.Sqrt(numberWeAreChecking);
            for (var currentDivisor = firstNumberToCheck; currentDivisor <= lastNumberToCheck; currentDivisor += 2) {
                if (numberWeAreChecking % currentDivisor == 0) {
                    return false;
                }
            }
            return true;
        }
    }
}