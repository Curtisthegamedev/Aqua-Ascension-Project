using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace AquaAscension.Utils
{
    public static class Rand
    {
        /// <summary>
        /// Return a float from 0...1
        /// </summary>
        public static float _01 
        {
            get => Random.Range(0f, 1f);
        }

        /// <summary>
        /// Return a true or false 50% of the time
        /// </summary>
        public static bool _5050
        {
            get => Random.Range(0, 2) == 1;
        }

        /// <summary>
        /// Return a float from from -1...1
        /// </summary>
        public static float insideUnitLine
        {
            get => Random.Range(-1f, 1f);
        }

        /// <summary>
        /// Return a uniform Vector2 from -0.5...0.5
        /// </summary>
        public static Vector2 insideUnitSquare
        {
            get => Random.Range(-0.5f, 0.5f) * Vector2.one;
        }

        /// <summary>
        /// Return a uniform Vector2 from 0...1
        /// </summary>
        public static Vector2 insideSquare
        {
            get => _01 * Vector2.one;
        }

        /// <summary>
        /// Return a uniform Vector3 from -0.5...0.5
        /// </summary>
        public static Vector3 insideUnitCube 
        {
            get => Random.Range(-0.5f, 0.5f) * Vector3.one;
        }

        /// <summary>
        /// Return a uniform Vector3 from 0...1
        /// </summary>
        public static Vector3 insideCube
        {
            get => _01 * Vector3.one;
        }

        /// <summary>
        /// Return a random Vector2 
        /// </summary>
        /// <param name="x">Multiplier</param>
        /// <param name="y">Multiplier</param>
        public static Vector2 M2(in float x = 1, in float y = 1) => new Vector2(_01 * x, _01 * y);

        /// <summary>
        /// Return a random Vector3 
        /// </summary>
        /// <param name="x">Multiplier</param>
        /// <param name="y">Multiplier</param>
        public static Vector3 M3(in float x = 1, in float y = 1, in float z = 1) => new Vector3(_01 * x, _01 * y, _01 * z);
        
        /// <summary>
        /// Returns a non-negative random integer from 0 - max[inclusive].
        /// </summary>
        /// <param name="max">[inclusive]</param>
        public static int Next(in int max) => Random.Range(0, max + 1);

        /// <summary>
        /// Return a random int given min [inclusive] and max [inclusive].
        /// Just like how it's supposed to be called! ヽ(ಠ_ಠ) ノ
        /// </summary>
        /// <param name="min">[inclusive]</param>
        /// <param name="max">[inclusive]</param>
        public static int Range(in int min, in int max) => Random.Range(min, max + 1);

        /// <summary>
        /// Return a true or false n% of the time
        /// </summary>
        /// <param name="probability">Probability between 0...1</param>
        public static bool Chance(in float probability) => Random.Range(0f, 1f) <= probability;

        /// <summary>
        /// Return a normal distibution random float number between min [inclusive] and max [inclusive] (Read Only).
        /// </summary>
        public static float Gaussian(in float min, in float max) => Random.Range((min + max) / 2f, (max - min) / 3f);

        /// <summary>
        /// Returns an array of random unique numbers from min...max range. 
        /// </summary>
        /// <param name="min">[inclusive]</param>
        /// <param name="max">[inclusive]</param>
        /// <param name="length">Length of the array</param>
        /// <returns></returns>
        public static int[] Unique(in int min, in int max)
        {
            List<int> u = new List<int>(System.Math.Abs(min - max));
            for(int i = min; i <= max; i++) u[i] = i;
            u.Shuffle();
            return u.ToArray();
        }

        public static int[] Unique(in int min, in int max, int length)
        {
            List<int> u = new List<int>();
            var range = System.Math.Abs(min - max);
            length = length > range ? range : length;
            while(u.Count <= length)
            {
                u.Add(Range(min, max));
                u = u.Distinct().ToList();
            }
            return u.ToArray();
        }
    }
}