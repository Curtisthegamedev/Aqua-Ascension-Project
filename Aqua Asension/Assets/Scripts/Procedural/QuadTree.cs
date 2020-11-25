using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AquaAscension.Utils;

namespace AquaAscension
{
    [System.Serializable]
    public class QuadTree
    {
        public enum Quadrant : int
        {
            NW, NE, SW, SE, None = -1
        }

        public enum Direction : int
        {
            N, S, E, W
        }

        public static QuadTree GetEqualOrGreaterNeighbour(QuadTree self, Direction direction = Direction.N)
        {
            if (self == null || self.parent == null) return null;

            // North
            if (direction == Direction.N)
            {
                if (self.parent.children[(int)Quadrant.SW] == self)
                    return self.parent.children[(int)Quadrant.NW];
                if (self.parent.children[(int)Quadrant.SE] == self)
                    return self.parent.children[(int)Quadrant.NE];

                var node = GetEqualOrGreaterNeighbour(self.parent, direction);
                if (node == null || node.IsLeaf) return node;

                if (self.parent.children[(int)Quadrant.NW] == self)
                    return node.children[(int)Quadrant.SW];
                else
                    return node.children[(int)Quadrant.SE];
            }
            // South
            else if (direction == Direction.S)
            {
                if (self.parent.children[(int)Quadrant.NW] == self)
                    return self.parent.children[(int)Quadrant.SW];
                if (self.parent.children[(int)Quadrant.NE] == self)
                    return self.parent.children[(int)Quadrant.SE];

                var node = GetEqualOrGreaterNeighbour(self.parent, direction);
                if (node == null || node.IsLeaf) return node;

                if (self.parent.children[(int)Quadrant.SW] == self)
                    return node.children[(int)Quadrant.NW];
                else
                    return node.children[(int)Quadrant.NE];
            }
            // East
            else if (direction == Direction.E)
            {
                if (self.parent.children[(int)Quadrant.NW] == self)
                    return self.parent.children[(int)Quadrant.NE];
                if (self.parent.children[(int)Quadrant.SW] == self)
                    return self.parent.children[(int)Quadrant.SE];

                var node = GetEqualOrGreaterNeighbour(self.parent, direction);
                if (node == null || node.IsLeaf) return node;

                if (self.parent.children[(int)Quadrant.NE] == self)
                    return node.children[(int)Quadrant.NW];
                else
                    return node.children[(int)Quadrant.SW];
            }
            // West
            else
            {
                if (self.parent.children[(int)Quadrant.NE] == self)
                    return self.parent.children[(int)Quadrant.NW];
                if (self.parent.children[(int)Quadrant.SE] == self)
                    return self.parent.children[(int)Quadrant.SW];

                var node = GetEqualOrGreaterNeighbour(self.parent, direction);
                if (node == null || node.IsLeaf) return node;

                if (self.parent.children[(int)Quadrant.NW] == self)
                    return node.children[(int)Quadrant.NE];
                else
                    return node.children[(int)Quadrant.SE];
            }
        }

        public static QuadTree GetEqualNeighbour(QuadTree self, Direction direction = Direction.N)
        {
            var neighbour = GetEqualOrGreaterNeighbour(self, direction);
            if (neighbour == null || !self.HasSameArea(neighbour)) return null;
            return neighbour;
        }

        public static List<QuadTree> LevelTraversal(QuadTree node, int level = int.MaxValue)
        {
            if (node == null) return null;

            if (level <= 1 || node.IsLeaf) return new List<QuadTree>(new[] { node });

            List<QuadTree> quadTreeList = new List<QuadTree>();

            if (node.parent == null) quadTreeList.Add(node);

            foreach (var child in node.children)
                quadTreeList.AddRange(LevelTraversal(child, level - 1));

            return quadTreeList;
        }

        public static bool Inside(QuadTree perimeter, QuadTree point) => !HasSameArea(perimeter, point) && perimeter.Boundary.Contains(point.Boundary.center);
        public static bool HasSameArea(QuadTree a, QuadTree b) => a.Area == b.Area;

        internal List<QuadTree> children;
        protected QuadTree parent;

        public Rect Boundary { get; protected set; }
        public float Area { get => Boundary.width * Boundary.height; }
        public bool IsLeaf { get => children.Count <= 0; }
        public bool HasParent { get => parent != null; }

        public QuadTree(Rect boundary, QuadTree parent = null)
        {
            Boundary = boundary;
            children = new List<QuadTree>();
            this.parent = parent;
        }

        public bool HasSameArea(QuadTree other) => HasSameArea(this, other);
        public bool Inside(QuadTree other) => Inside(this, other);
        public QuadTree GetEqualOrGreaterNeighbour(Direction direction = Direction.N) => GetEqualOrGreaterNeighbour(this, direction);
        public QuadTree GetEqualNeighbour(Direction direction = Direction.N) => GetEqualNeighbour(this, direction);
        public List<QuadTree> LevelTraversal(int level = int.MaxValue) => LevelTraversal(this, level);

        //! Fix Wire Drawing, use center, and width, height w/o multiplying by 2...
        public void DrawGizmos()
        {
            Gizmos.DrawWireCube(new Vector3(Boundary.x, Boundary.y), new Vector3(Boundary.width * 2, Boundary.height * 2));
            if (!IsLeaf) children.ForEach(child => child.DrawGizmos());
        }

        public void Generate(int iterations, int totalIterations = -1)
        {
            if (iterations <= 0) return;

            if (totalIterations <= 0) totalIterations = iterations;

            if (Rand.Chance(1f - 1f / iterations / totalIterations))
            {
                iterations--;
                Subdivide();
                children.ForEach(child => child.Generate(iterations, totalIterations));
            }
        }

        protected void Subdivide()
        {
            children.Add(new QuadTree(new Rect(Boundary.xMin - Boundary.width / 2f, Boundary.yMin - Boundary.height / 2f, Boundary.width / 2f, Boundary.height / 2f), this));
            children.Add(new QuadTree(new Rect(Boundary.xMin + Boundary.width / 2f, Boundary.yMin - Boundary.height / 2f, Boundary.width / 2f, Boundary.height / 2f), this));
            children.Add(new QuadTree(new Rect(Boundary.xMin - Boundary.width / 2f, Boundary.yMin + Boundary.height / 2f, Boundary.width / 2f, Boundary.height / 2f), this));
            children.Add(new QuadTree(new Rect(Boundary.xMin + Boundary.width / 2f, Boundary.yMin + Boundary.height / 2f, Boundary.width / 2f, Boundary.height / 2f), this));
        }

        public void Flatten() => children.Clear();

        /// <summary>
        /// This function returns a rect pair  
        /// </summary>
        public (Rect, Rect) MergeNode(Direction direction)
        {
            var neighbour = this.GetEqualNeighbour(direction);
            return (this.Boundary, neighbour.Boundary);
        }

        [System.Obsolete("This will give you unexpected values...")]
        public QuadTree GetNode(Queue<Quadrant> quadrants)
        {
            try
            {
                if (IsLeaf || quadrants.Count <= 0) return this;
                var quadrant = quadrants.Dequeue();
                if (quadrant == Quadrant.None) return this;
                return children[(int)quadrant].GetNode(quadrants);
            }
            catch (System.Exception ex) { return this; }
        }
    }
}