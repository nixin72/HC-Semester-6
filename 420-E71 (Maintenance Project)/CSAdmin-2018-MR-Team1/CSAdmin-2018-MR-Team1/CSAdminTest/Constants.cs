using System;

namespace CSAdminTest
{
	public static class Constants
	{
		public const string LongString =
			"It's important to me that you're happy. Let's have a happy little tree in here. There isn't a rule. You just practice and find out which way works best for you. Think about a cloud. Just float around and be there. How do you make a round circle with a square knife? That's your challenge for the day. You can spend all day playing with mountains. Trees grow however makes them happy. Here we're limited by the time we have. You got your heavy coat out yet? It's getting colder. That's a crooked tree. We'll send him to Washington. Maybe he has a little friend that lives right over here. Let the paint work. Everyone is going to see things differently - and that's the way it should be. Let's get crazy. The shadows are just like the highlights, but we're going in the opposite direction. Just let your mind wander and enjoy. This should make you happy. Put your feelings into it, your heart, it's your world. We spend so much of our life looking - but never seeing. Let's do it again then, what the ";

		public const string NullString = null;

		public const string LongWhiteSpaceString =
			"                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ";

		public readonly static Random rand;

		static Constants()
		{
			rand = new Random();
		}

		public static int RandInt()
		{
			return rand.Next(int.MinValue, int.MaxValue);
		}

		public static bool RandBool()
		{
			return rand.Next(2) == 0;
		}
	}
}