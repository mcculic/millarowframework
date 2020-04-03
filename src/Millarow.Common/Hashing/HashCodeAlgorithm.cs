using System.Collections.Generic;

namespace Millarow.Hashing
{
    public abstract class HashCodeAlgorithm
    {
        protected abstract void CombineHash<T>(ref int hash, T value);

        protected virtual int SeedHash()
        {
            return 0;
        }

        protected virtual int FinalizeHash(int hash)
        {
            return hash;
        }

        public int Get<T1>(T1 v1)
        {
            var hash = SeedHash();

            CombineHash(ref hash, v1);

            return FinalizeHash(hash);
        }

        public int Get<T1, T2>(T1 v1, T2 v2)
        {
            var hash = SeedHash();

            CombineHash(ref hash, v1);
            CombineHash(ref hash, v2);

            return FinalizeHash(hash);
        }

        public int Get<T1, T2, T3>(T1 v1, T2 v2, T3 v3)
        {
            var hash = SeedHash();

            CombineHash(ref hash, v1);
            CombineHash(ref hash, v2);
            CombineHash(ref hash, v3);

            return FinalizeHash(hash);
        }

        public int Get<T1, T2, T3, T4>(T1 v1, T2 v2, T3 v3, T4 v4)
        {
            var hash = SeedHash();

            CombineHash(ref hash, v1);
            CombineHash(ref hash, v2);
            CombineHash(ref hash, v3);
            CombineHash(ref hash, v4);

            return FinalizeHash(hash);
        }

        public int Get<T1, T2, T3, T4, T5>(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5)
        {
            var hash = SeedHash();

            CombineHash(ref hash, v1);
            CombineHash(ref hash, v2);
            CombineHash(ref hash, v3);
            CombineHash(ref hash, v4);
            CombineHash(ref hash, v5);

            return FinalizeHash(hash);
        }

        public int Get<T1, T2, T3, T4, T5, T6>(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6)
        {
            var hash = SeedHash();

            CombineHash(ref hash, v1);
            CombineHash(ref hash, v2);
            CombineHash(ref hash, v3);
            CombineHash(ref hash, v4);
            CombineHash(ref hash, v5);
            CombineHash(ref hash, v6);

            return FinalizeHash(hash);
        }

        public int Get<T1, T2, T3, T4, T5, T6, T7>(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7)
        {
            var hash = SeedHash();

            CombineHash(ref hash, v1);
            CombineHash(ref hash, v2);
            CombineHash(ref hash, v3);
            CombineHash(ref hash, v4);
            CombineHash(ref hash, v5);
            CombineHash(ref hash, v6);
            CombineHash(ref hash, v7);

            return FinalizeHash(hash);
        }

        public int Get<T1, T2, T3, T4, T5, T6, T7, T8>(T1 v1, T2 v2, T3 v3, T4 v4, T5 v5, T6 v6, T7 v7, T8 v8)
        {
            var hash = SeedHash();

            CombineHash(ref hash, v1);
            CombineHash(ref hash, v2);
            CombineHash(ref hash, v3);
            CombineHash(ref hash, v4);
            CombineHash(ref hash, v5);
            CombineHash(ref hash, v6);
            CombineHash(ref hash, v7);
            CombineHash(ref hash, v8);

            return FinalizeHash(hash);
        }

        public int GetFromMany<T>(IEnumerable<T> values)
        {
            values.AssertNotNull(nameof(values));

            var hash = SeedHash();

            foreach (var value in values)
                CombineHash(ref hash, value);

            return FinalizeHash(hash);
        }

        public int Get(params object[] values)
        {
            values.AssertNotNull(nameof(values));

            var hash = SeedHash();

            foreach (var value in values)
                CombineHash(ref hash, value);

            return FinalizeHash(hash);
        }
    }
}