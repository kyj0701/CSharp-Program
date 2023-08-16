using System;
using System.Collections.Generic;

namespace Blackjack
{
    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Rank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    // 카드 한 장을 표현하는 클래스
    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }

        public Card(Suit s, Rank r)
        {
            Suit = s;
            Rank = r;
        }

        public int GetValue()
        {
            if ((int)Rank <= 10)
            {
                return (int)Rank;
            }
            else if ((int)Rank <= 13)
            {
                return 10;
            }
            else
            {
                return 11;
            }
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }

    // 덱을 표현하는 클래스
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();

            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card(s, r));
                }
            }

            Shuffle();
        }

        public void Shuffle()
        {
            Random rand = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int j = rand.Next(i, cards.Count);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public Card DrawCard()
        {
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }

    // 패를 표현하는 클래스
    public class Hand
    {
        private List<Card> cards;

        public Hand()
        {
            cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int GetTotalValue()
        {
            int total = 0;
            int aceCount = 0;

            foreach (Card card in cards)
            {
                if (card.Rank == Rank.Ace)
                {
                    aceCount++;
                }
                total += card.GetValue();
            }

            while (total > 21 && aceCount > 0)
            {
                total -= 10;
                aceCount--;
            }

            return total;
        }
    }

    // 플레이어를 표현하는 클래스
    public class Player
    {
        public Hand Hand { get; private set; }

        public Player()
        {
            Hand = new Hand();
        }

        public Card DrawCardFromDeck(Deck deck)
        {
            Card drawnCard = deck.DrawCard();
            Hand.AddCard(drawnCard);
            return drawnCard;
        }
    }

    // 여기부터는 학습자가 작성
    // 딜러 클래스를 작성하고, 딜러의 행동 로직을 구현하세요.
    public class Dealer : Player
    {
        // 딜러는 총점이 17점 미만일 때 계속해서 카드를 뽑는다
        public void DrawingCards(Deck deck)
        {
            while (Hand.GetTotalValue() < 17)
            {
                Card draw = DrawCardFromDeck(deck);
                Console.WriteLine($"딜러는 '{draw}'을(를) 뽑았습니다. 현재 총합은 {Hand.GetTotalValue()}점입니다.");
            }
        }
    }

    // 블랙잭 게임을 구현하세요. 
    public class Blackjack
    {
        private Player player;
        private Dealer dealer;
        private Deck deck;

        public void Play()
        {
            deck = new Deck();
            player = new Player();
            dealer = new Dealer();

            for (int i = 0; i < 2; i++)
            {
                player.DrawCardFromDeck(deck);
                dealer.DrawCardFromDeck(deck);
            }

            Console.WriteLine("게임 시작!");
            Console.WriteLine($"플레이어의 카드 합: {player.Hand.GetTotalValue()}");
            Console.WriteLine($"딜러의 카드 합: {dealer.Hand.GetTotalValue()}");

            // 플레이어의 차례
            while (player.Hand.GetTotalValue() < 21)
            {
                Console.Write("카드를 더 뽑으시겠습니까? ( y(Y) / n(N) ): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "y")
                {
                    Card draw = player.DrawCardFromDeck(deck);
                    Console.WriteLine($"'{draw}'을(를) 뽑았습니다. 현재 총합 {player.Hand.GetTotalValue()}점.");
                }
                else
                {
                    break;
                }
            }

            // 딜러의 차례
            Console.WriteLine("딜러의 차례입니다.");
            dealer.DrawingCards(deck);
            Console.WriteLine($"딜러의 총합 {dealer.Hand.GetTotalValue()}점.");

            // 승자 판정
            if (player.Hand.GetTotalValue() > 21)
            {
                Console.WriteLine("플레이어의 카드 합이 21점을 초과했습니다. 딜러의 승리입니다.");
            }
            else if (dealer.Hand.GetTotalValue() > 21)
            {
                Console.WriteLine("딜러의 카드 합이 21점을 초과했습니다. 플레이어의 승리입니다.");
            }
            else if (player.Hand.GetTotalValue() > dealer.Hand.GetTotalValue())
            {
                Console.WriteLine("플레이어의 카드 합이 더 높습니다. 플레이어의 승리입니다.");
            }
            else
            {
                Console.WriteLine("딜러의 카드 합이 더 높거나 같습니다. 딜러의 승리입니다.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Blackjack game = new Blackjack();
            game.Play();
        }
    }
}