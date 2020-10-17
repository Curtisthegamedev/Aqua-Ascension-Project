
using UnityEngine;

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
        public static Vector2 M2(float x = 1, float y = 1) => new Vector2(_01 * x, _01 * y);

        /// <summary>
        /// Return a random Vector3 
        /// </summary>
        /// <param name="x">Multiplier</param>
        /// <param name="y">Multiplier</param>
        public static Vector3 M3(float x = 1, float y = 1, float z = 1) => new Vector3(_01 * x, _01 * y, _01 * z);
        
        /// <summary>
        /// Return a true or false n% of the time
        /// </summary>
        /// <param name="probability">Probability between 0...1</param>
        public static bool Chance(float probability) => Random.Range(0f, 1f) <= probability;

        /// <summary>
        /// Return a normal distibution random float number between min [inclusive] and max [inclusive] (Read Only).
        /// </summary>
        public static float Gaussian(float min, float max) => Random.Range((min + max) / 2f, (max - min) / 3f);
    }
}