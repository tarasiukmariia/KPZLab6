using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Resources;

namespace MAD_MAN_Ping_Pong
{
    
    public partial class Form1 : Form

    {
        private ICommand moveCommand;
        public Form1()
        {
            InitializeComponent();
            moveCommand = new MovePlayerCommand(P1);
        }

        //визначення класу ігрового процесу пінг-понг

        class game_play
        {
            public int p1_score;
            public int p2_score;
            public int wall;
            public int accept = 0;
            public int regulate = 0;
            public int again = 0;
            public int part = 1;
            public int gx, gy;
            public int first = 0;
            public double x, y;
            public double x1, y1;
            public double a, b;
            public double speed = 5;
            public double x2, y2;
            public double x0, y0;
        }

        game_play main_scene = new game_play();


        // клас опису допоміжних змінних
        class ident_support // визначає додаткові змінні та налаштування, які підтримують гру. Цей клас містить змінні, такі як обмеження, швидкість та прапорці, які використовуються для контролю руху платформ та м'яча в грі.
        {
            public int limit_Pad = 170;
            public int limit_Ball = 245;
            public int x = 227, y = 120;

            public int computer_won = 0;
            public int player_won = 0;

            public int speed_Top;
            public int speed_Left;

            public bool up = false;
            public bool down = false;
            public bool game = false;
        }


       

        /*********** клас визначення логіки ігрового процесу *************************/
        class game_logic // включає методи, які відповідають за обробку логіки гри та подій. У цьому класі реалізовані методи для обробки зіткнень м'яча з платформами, руху м'яча, розрахунку рахунків та управління станом гри.
        {
            private static game_logic instance;
            private ident_support variables = new ident_support();
            private Form1 childForm = new Form1();

          
            private game_logic() { }

            public static game_logic GetInstance()
            {
                if (instance == null)
                {
                    instance = new game_logic();
                }
                return instance;
            }

            public void Collision(PictureBox Paddle)
            {
                switch (true)
                {
                    case true when Upper(Paddle):
                        variables.speed_Top = Negative(4, 6);
                        variables.speed_Left = AdjustCoordinates(5, 6);
                        break;
                    case true when High(Paddle):
                        variables.speed_Top = Negative(2, 3);
                        variables.speed_Left = AdjustCoordinates(6, 7);
                        break;
                    case true when Middle(Paddle):
                        variables.speed_Top = 0;
                        variables.speed_Left = AdjustCoordinates(5, 5);
                        break;

                }
                Edge();
            }


            public void Edge()
            {
                
                // класс Form1
                

                if (childForm.Ball.Location.X < childForm.Width / 2)
                {
                    if (childForm.Ball.Location.X < 0 + childForm.Ball.Height / 3)
                    {
                        variables.speed_Left *= -1;
                    }
                }
                else if (childForm.Ball.Location.X > childForm.Width / 2)
                {
                    if (childForm.Ball.Location.X > 50 + (childForm.Ball.Width / 3))
                    {
                        variables.speed_Left *= -1;
                    }
                }
            }

            public int AdjustCoordinates(int i, int n)
            {
                int res = 0;
                

                if (childForm.Ball.Location.X < childForm.Width / 2)
                {
                    res = 5;
                }
                else if (childForm.Ball.Location.X > childForm.Width / 2)
                {
                    res = Negative(i, n);
                }
                return res;
            }
            public int Negative(int i, int n)
            {
                int myval = (-1);

                return myval;
            }
            public bool Upper(PictureBox Pad)
            {
                
                return childForm.Ball.Location.Y >= Pad.Top - childForm.Ball.Height && childForm.Ball.Location.Y <= Pad.Top + childForm.Ball.Height;
            }
            public bool High(PictureBox Pad)
            {

                return childForm.Ball.Location.Y > Pad.Top + childForm.Ball.Height && childForm.Ball.Location.Y <= Pad.Top + 2 * childForm.Ball.Height;
            }
            public bool Middle(PictureBox Pad)
            {
                return childForm.Ball.Location.Y > Pad.Top + 2 * childForm.Ball.Height && childForm.Ball.Location.Y <= Pad.Top + 3 * childForm.Ball.Height;
            }
            public bool Low(PictureBox Pad)
            {
                return childForm.Ball.Location.Y > Pad.Top + 3 * childForm.Ball.Height && childForm.Ball.Location.Y <= Pad.Top + 4 * childForm.Ball.Height;
            }
            public bool Bot(PictureBox Pad)
            {
                return childForm.Ball.Location.Y > Pad.Top + 4 * childForm.Ball.Height && childForm.Ball.Location.Y <= Pad.Bottom + childForm.Ball.Height;
            }
            public void HitBorder()
            {
                if (childForm.Ball.Location.Y <= 0 || childForm.Ball.Location.Y >= variables.limit_Ball)
                {
                    variables.speed_Top *= -1;
                }
            }


            public void EndGame()
            {

                variables.game = false;
                variables.player_won = 0;
                variables.computer_won = 0;
                childForm.label1.Text = variables.player_won.ToString();
                childForm.button1.Visible = true;
            }
        }
        /*    */





        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        //P1 move
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse == 1)
            {
                moveCommand.Execute(Cursor.Position.Y);
            }
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse == 1)
            {
                moveCommand.Execute(Cursor.Position.Y);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouse == 1)
            {
                moveCommand.Execute(Cursor.Position.Y);
            }
        }

        // Additional methods to switch between player platforms (P1 and P2)
        public void SetMoveCommand(Control platform)
        {
            moveCommand = new MovePlayerCommand(platform);
        }

        //P1 move end

        private void Form1_Load(object sender, EventArgs e)
        {
            Ball.Location = new Point(panel2.Size.Width / 2 - 5, panel3.Location.Y / 2 - panel2.Size.Height);
            pict.Location = new Point(panel2.Size.Width / 2 + (pict.Size.Width / 2), pict.Location.Y);
        }

        
        Random rand = new Random();

        private void Start_Click(object sender, EventArgs e)
        {
            main_scene.first++;
            if (main_scene.first == 1)
            {
                main_scene.gx = Score.Location.X;
                main_scene.gy = Score.Location.Y;
            }
            Score.Location = new Point(main_scene.gx, main_scene.gy);
            Score.Text = "0 : 0";
            main_scene.p1_score = 0;
            main_scene.p2_score = 0;
            Ball.Location = new Point(panel2.Size.Width / 2 - 5, panel3.Location.Y / 2 - panel2.Size.Height);
            main_scene.x = Ball.Location.X;
            main_scene.y = Ball.Location.Y;
            main_scene.x1 = 0;
            main_scene.y1 = 0;
            int ok = 0;
            while (ok == 0)
            {
                main_scene.wall = rand.Next(1, 4);
                if (main_scene.wall == 1)
                {
                    main_scene.y1 = panel2.Size.Height;
                    main_scene.x1 = rand.Next(P1.Size.Width + P1.Location.X, panel2.Size.Width / 2 - 100);
                }
                if (main_scene.wall == 2)
                {
                    main_scene.x1 = P1.Size.Width + P1.Location.X;
                    main_scene.y1 = rand.Next(panel2.Size.Height, panel3.Location.Y + 1 - Ball.Size.Height);
                }
                if (main_scene.wall == 3)
                {
                    main_scene.y1 = panel3.Location.Y - Ball.Size.Height;
                    main_scene.x1 = rand.Next(P1.Size.Width + P1.Location.X, panel2.Size.Width / 2 - 100);
                }

                if (main_scene.x1 != main_scene.x && main_scene.y1 != main_scene.y)
                {
                    main_scene.a = (main_scene.y1 - main_scene.y) / (main_scene.x1 - main_scene.x);
                    main_scene.b = main_scene.y1 - (main_scene.a * main_scene.x1);
                    main_scene.speed = 5;
                    P2_GUI.Enabled = false;
                    Ball_Move.Enabled = true;
                    ok = 1;
                    main_scene.accept = 0;
                    main_scene.regulate = 0;
                    main_scene.again = 0;
                }
            }
            
            
        }
        private void Ball_Move_Tick(object sender, EventArgs e)
        {
            if (main_scene.accept == 0 && main_scene.again ==0)
            {
                if (main_scene.x1 < main_scene.x)
                {
                    if (main_scene.x - main_scene.speed > main_scene.x1)
                    {
                        main_scene.x -= main_scene.speed;
                        main_scene.y = main_scene.a * main_scene.x + main_scene.b;
                        Ball.Location = new Point((int)main_scene.x, (int)main_scene.y);
                    }
                    else
                    {
                        main_scene.x = main_scene.x1;
                        main_scene.y = main_scene.y1;
                        Ball.Location = new Point((int)main_scene.x, (int)main_scene.y);
                        //respingere
                        main_scene.accept = 1;
                        Respingere();
                    }

                }
                else
                {
                    if (main_scene.x1 > main_scene.x)
                    {
                        if (main_scene.x + main_scene.speed < main_scene.x1)
                        {
                            main_scene.x += main_scene.speed;
                            main_scene.y = main_scene.a * main_scene.x + main_scene.b;
                            Ball.Location = new Point((int)main_scene.x, (int)main_scene.y);
                        }
                        else
                        {
                            main_scene.x = main_scene.x1;
                            main_scene.y = main_scene.y1;
                            Ball.Location = new Point((int)main_scene.x, (int)main_scene.y);
                            //respingere
                            main_scene.accept = 1;
                            Respingere();
                        }
                    }
                    else
                    {
                        if (main_scene.x1 == main_scene.x)
                        {
                            Ball.Location = new Point((int)main_scene.x, (int)main_scene.y);
                            //respingere
                            main_scene.accept = 1;
                            Respingere();
                        }
                    }
                }
                if (_command == 1)
                {

                    if (Level.Text == "ХАРД")
                    {
                        if (main_scene.y <= panel3.Location.Y - P2.Size.Height)
                        {
                            P2.Location = new Point(P2.Location.X, (int)main_scene.y);
                        }
                        else
                        {
                            P2.Location = new Point(P2.Location.X, panel3.Location.Y - P2.Size.Height - 5);
                        }
                    }
                }
                
            }
        }


        public interface IObserver
        {
            void Update();
        }

        public class Observable
        {
            private List<IObserver> observers = new List<IObserver>();

            public void Subscribe(IObserver observer)
            {
                observers.Add(observer);
            }

            public void Unsubscribe(IObserver observer)
            {
                observers.Remove(observer);
            }

            public void Notify()
            {
                foreach (var observer in observers)
                {
                    observer.Update();
                }
            }
        }


        public class ConcreteObserver : IObserver
        {
            public void Update()
            {
                Console.WriteLine("ConcreteObserver отримав оновлення!");
            }
        }
        public interface IState
        {
            void Handle();
        }

        public class ConcreteStateA : IState
        {
            public void Handle()
            {
                Console.WriteLine("Перехід до стану A");
            }
        }

        public class ConcreteStateB : IState
        {
            public void Handle()
            {
                Console.WriteLine("Перехід до стану B");
            }
        }


        public class Context
        {
            private IState state;

            public Context(IState initialState)
            {
                state = initialState;
            }

            public void ChangeState(IState newState)
            {
                state = newState;
            }

            public void Request()
            {
                state.Handle();
            }
        }


        public class StatePatternExample
        {
            public void RunExample()
            {
                var context = new Context(new ConcreteStateA());


                context.Request();

                context.ChangeState(new ConcreteStateB());


                context.Request();
            }
        }

        public abstract class Factory
        {
            public abstract Product CreateProduct();
        }


        public class ConcreteFactory : Factory
        {
            public override Product CreateProduct()
            {
                return new ConcreteProduct();
            }
        }


        public abstract class Product
        {
            public abstract void Operation();
        }


        public class ConcreteProduct : Product
        {
            public override void Operation()
            {
                Console.WriteLine("Викликано операцію у конкретному продукті.");
            }
        }


        public class FactoryMethodExample
        {
            public void RunExample()
            {

                Factory factory = new ConcreteFactory();


                Product product = factory.CreateProduct();


                product.Operation();
            }
        }

       
        public class ObserverPatternExample
        {
            public void RunExample()
            {
                var observable = new Observable();

                var observer = new ConcreteObserver();

                observable.Subscribe(observer);

                observable.Notify();


                observable.Unsubscribe(observer);
            }
        }



        private void Respingere()
        {
            if (main_scene.again == 0)
            {
                main_scene.regulate++;
                if (main_scene.wall == 1)
                {
                    main_scene.y2 = main_scene.y + 10;
                    main_scene.x2 = (main_scene.y2 - main_scene.b) / main_scene.a;
                    main_scene.y1 = main_scene.y2;
                    main_scene.x1 = 2 * main_scene.x - main_scene.x2;
                    main_scene.a = (main_scene.y1 - main_scene.y) / (main_scene.x1 - main_scene.x);
                    main_scene.b = main_scene.y - (main_scene.a * main_scene.x);

                    main_scene.x0 = P1.Location.X + P1.Size.Width;
                    main_scene.y0 = main_scene.a * main_scene.x0 + main_scene.b;
                    main_scene.wall = 2;
                    if (main_scene.y0 < panel2.Size.Height || main_scene.y0 > panel3.Location.Y - Ball.Size.Height)
                    {
                        main_scene.y0 = panel3.Location.Y - Ball.Size.Height;
                        main_scene.x0 = (main_scene.y0 - main_scene.b) / main_scene.a;
                        main_scene.wall = 3;
                        if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                        {
                            main_scene.x0 = P2.Location.X - Ball.Size.Width;
                            main_scene.y0 = main_scene.a * main_scene.x0 + main_scene.b;
                            main_scene.wall = 4;
                            if (_command == 1)
                            {
                                if (main_scene.y0 < panel3.Location.Y - P2.Size.Height - 5)
                                {
                                    sy = (int)main_scene.y0;
                                }
                                else
                                {
                                    if (main_scene.y0 >= panel3.Location.Y - P2.Size.Height - 5)
                                    {
                                        sy = panel3.Location.Y - P2.Size.Height - 5;
                                    }
                                }
                                P2_GUI.Enabled = true;

                            }
                        }
                    }
                    main_scene.x1 = main_scene.x0;
                    main_scene.y1 = main_scene.y0;
                }
                else
                {
                    if (main_scene.wall == 2)
                    {
                        if (main_scene.y <= P1.Size.Height + P1.Location.Y && main_scene.y >= P1.Location.Y - Ball.Size.Height)
                        {
                            main_scene.x2 = main_scene.x + 10;
                            main_scene.y2 = main_scene.x2 * main_scene.a + main_scene.b;
                            main_scene.x1 = main_scene.x2;
                            main_scene.y1 = 2 * main_scene.y - main_scene.y2;
                            main_scene.a = (main_scene.y1 - main_scene.y) / (main_scene.x1 - main_scene.x);
                            main_scene.b = main_scene.y1 - (main_scene.a * main_scene.x1);

                            main_scene.y0 = panel2.Size.Height;
                            main_scene.x0 = (main_scene.y0 - main_scene.b) / main_scene.a;
                            main_scene.wall = 1;
                            if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                            {
                                main_scene.y0 = panel3.Location.Y - Ball.Size.Height;
                                main_scene.x0 = (main_scene.y0 - main_scene.b) / main_scene.a;
                                main_scene.wall = 3;
                                if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                                {
                                    main_scene.x0 = P2.Location.X - Ball.Size.Width;
                                    main_scene.y0 = main_scene.a * main_scene.x0 + main_scene.b;
                                    main_scene.wall = 4;
                                    if (_command == 1)
                                    {
                                        if (main_scene.y0 < panel3.Location.Y - P2.Size.Height - 5)
                                        {
                                            sy = (int)main_scene.y0;
                                        }
                                        else
                                        {
                                            if (main_scene.y0 >= panel3.Location.Y - P2.Size.Height - 5)
                                            {
                                                sy = panel3.Location.Y - P2.Size.Height - 5;
                                            }
                                        }
                                        P2_GUI.Enabled = true;
                                    }
                                }
                            }
                            main_scene.x1 = main_scene.x0;
                            main_scene.y1 = main_scene.y0;
                        }
                        else
                        {

                            main_scene.p2_score++;
                            if (main_scene.p2_score < 7)
                            {
                                main_scene.again = 1;
                                Score.Text = "" + main_scene.p1_score + " : " + main_scene.p2_score;
                                main_scene.part = 1;
                                //refresh
                                refresh();
                            }
                            else
                            {
                                if (main_scene.p2_score == 7)
                                {
                                    Ball_Move.Enabled = false;
                                    Score.Location = new Point(Score.Location.X-160,Score.Location.Y);
                                    if (_command == 1)
                                    {
                                        Score.Text = "Blue виграв!";
                                    }
                                    else
                                    {
                                        Score.Text = "Pink виграв!";
                                    }
                                    Ball.Location = new Point(panel2.Size.Width / 2 - 5, panel3.Location.Y / 2 - panel2.Size.Height);

                                }
                            }
                        }
                    }
                    else
                    {
                        if (main_scene.wall == 3)
                        {
                            main_scene.y2 = main_scene.y - 10;
                            main_scene.x2 = (main_scene.y2 - main_scene.b) / main_scene.a;
                            main_scene.y1 = main_scene.y2;
                            main_scene.x1 = 2 * main_scene.x - main_scene.x2;
                            main_scene.a = (main_scene.y1 - main_scene.y) / (main_scene.x1 - main_scene.x);
                            main_scene.b = main_scene.y - (main_scene.a * main_scene.x);

                            main_scene.x0 = P1.Location.X + P1.Size.Width;
                            main_scene.y0 = main_scene.a * main_scene.x0 + main_scene.b;
                            main_scene.wall = 2;
                            if (main_scene.y0 < panel2.Size.Height || main_scene.y0 > panel3.Location.Y - Ball.Size.Height)
                            {
                                main_scene.y0 = panel2.Size.Height;
                                main_scene.x0 = (main_scene.y0 - main_scene.b) / main_scene.a;
                                main_scene.wall = 1;
                                if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                                {
                                    main_scene.x0 = P2.Location.X - Ball.Size.Width;
                                    main_scene.y0 = main_scene.a * main_scene.x0 + main_scene.b;
                                    main_scene.wall = 4;
                                    if (_command == 1)
                                    {
                                        if (main_scene.y0 < panel3.Location.Y - P2.Size.Height - 5)
                                        {
                                            sy = (int)main_scene.y0;
                                        }
                                        else
                                        {
                                            if (main_scene.y0 >= panel3.Location.Y - P2.Size.Height - 5)
                                            {
                                                sy = panel3.Location.Y - P2.Size.Height - 5;
                                            }
                                        }
                                        P2_GUI.Enabled = true;

                                    }
                                }
                            }
                            main_scene.x1 = main_scene.x0;
                            main_scene.y1 = main_scene.y0;
                        }
                        else
                        {
                            if (main_scene.wall == 4)
                            {
                                if (main_scene.y <= P2.Size.Height + P2.Location.Y && main_scene.y >= P2.Location.Y - Ball.Size.Height)
                                {
                                    main_scene.x2 = main_scene.x - 10;
                                    main_scene.y2 = main_scene.x2 * main_scene.a + main_scene.b;
                                    main_scene.x1 = main_scene.x2;
                                    main_scene.y1 = 2 * main_scene.y - main_scene.y2;

                                    main_scene.a = (main_scene.y1 - main_scene.y) / (main_scene.x1 - main_scene.x);
                                    main_scene.b = main_scene.y1 - (main_scene.a * main_scene.x1);
                                    main_scene.y0 = panel2.Size.Height;
                                    main_scene.x0 = (main_scene.y0 - main_scene.b) / main_scene.a;
                                    main_scene.wall = 1;
                                    if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                                    {
                                        main_scene.y0 = panel3.Location.Y - Ball.Size.Height;
                                        main_scene.x0 = (main_scene.y0 - main_scene.b) / main_scene.a;
                                        main_scene.wall = 3;
                                        if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                                        {
                                            main_scene.x0 = P1.Location.X + P1.Size.Width;
                                            main_scene.y0 = main_scene.a * main_scene.x0 + main_scene.b;
                                            main_scene.wall = 2;
                                        }
                                    }
                                    main_scene.x1 = main_scene.x0;
                                    main_scene.y1 = main_scene.y0;
                                }
                                else
                                {

                                    main_scene.p1_score++;
                                    if (main_scene.p1_score < 7)
                                    {
                                        main_scene.again = 1;
                                        Score.Text = "" + main_scene.p1_score + " : " + main_scene.p2_score;
                                        main_scene.part = 2;
                                        //refresh
                                        refresh();
                                    }
                                    else
                                    {
                                        if (main_scene.p1_score == 7)
                                        {
                                            Ball_Move.Enabled = false;
                                            Score.Location = new Point(Score.Location.X - 160, Score.Location.Y);
                                            Score.Text = "Pink переміг!";
                                            Ball.Location = new Point(panel2.Size.Width / 2 - 5, panel3.Location.Y / 2 - panel2.Size.Height);

                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (Level.Text == "ВАЖКИЙ")
                    {
                        double _x1 = main_scene.x1, _y1 = main_scene.y1, _x2 = main_scene.x2, _y2 = main_scene.y2, _x = main_scene.x1,_y= main_scene.y1,_a= main_scene.a,_b= main_scene.b;
                        if (main_scene.wall == 1)
                        {
                            _y2 = _y + 10;
                            _x2 = (_y2 - _b) / _a;
                            _y1 = _y2;
                            _x1 = 2 * _x - _x2;
                            _a = (_y1 - _y) / (_x1 - _x);
                            _b = _y - (_a * _x);

                            main_scene.x0 = P1.Location.X + P1.Size.Width;
                            main_scene.y0 = _a * main_scene.x0 + _b;
                            if (main_scene.y0 < panel2.Size.Height || main_scene.y0 > panel3.Location.Y - Ball.Size.Height)
                            {
                                main_scene.y0 = panel3.Location.Y - Ball.Size.Height;
                                main_scene.x0 = (main_scene.y0 - _b) / _a;
                                if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                                {
                                    main_scene.x0 = P2.Location.X - Ball.Size.Width;
                                    main_scene.y0 = _a * main_scene.x0 + _b;
                                    if (_command == 1)
                                    {
                                        if (main_scene.y0 < panel3.Location.Y - P2.Size.Height - 5)
                                        {
                                            sy = (int)main_scene.y0;
                                        }
                                        else
                                        {
                                            if (main_scene.y0 >= panel3.Location.Y - P2.Size.Height - 5)
                                            {
                                                sy = panel3.Location.Y - P2.Size.Height - 5;
                                            }
                                        }
                                        P2_GUI.Enabled = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (main_scene.wall == 2)
                            {
                                _x2 = _x + 10;
                                _y2 = _x2 * _a + _b;
                                _x1 = _x2;
                                _y1 = 2 * _y - _y2;
                                _a = (_y1 - _y) / (_x1 - _x);
                                _b = _y1 - (_a * _x1);

                                main_scene.y0 = panel2.Size.Height;
                                main_scene.x0 = (main_scene.y0 - _b) / _a;
                                if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                                {
                                    main_scene.y0 = panel3.Location.Y - Ball.Size.Height;
                                    main_scene.x0 = (main_scene.y0 - _b) / _a;
                                    if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                                    {
                                        main_scene.x0 = P2.Location.X - Ball.Size.Width;
                                        main_scene.y0 = _a * main_scene.x0 + _b;
                                        if (_command == 1)
                                        {
                                            if (main_scene.y0 < panel3.Location.Y - P2.Size.Height - 5)
                                            {
                                                sy = (int)main_scene.y0;
                                            }
                                            else
                                            {
                                                if (main_scene.y0 >= panel3.Location.Y - P2.Size.Height - 5)
                                                {
                                                    sy = panel3.Location.Y - P2.Size.Height - 5;
                                                }
                                            }
                                            P2_GUI.Enabled = true;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                if (main_scene.wall == 3)
                                {
                                    _y2 = _y - 10;
                                    _x2 = (_y2 - _b) / _a;
                                    _y1 = _y2;
                                    _x1 = 2 * _x - _x2;
                                    _a = (_y1 - _y) / (_x1 - _x);
                                    _b = _y - (_a * _x);

                                    main_scene.x0 = P1.Location.X + P1.Size.Width;
                                    main_scene.y0 = _a * main_scene.x0 + _b;
                                    if (main_scene.y0 < panel2.Size.Height || main_scene.y0 > panel3.Location.Y - Ball.Size.Height)
                                    {
                                        main_scene.y0 = panel2.Size.Height;
                                        main_scene.x0 = (main_scene.y0 - _b) / _a;
                                        if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                                        {
                                            main_scene.x0 = P2.Location.X - Ball.Size.Width;
                                            main_scene.y0 = _a * main_scene.x0 + _b;
                                            if (_command == 1)
                                            {
                                                if (main_scene.y0 < panel3.Location.Y - P2.Size.Height - 5)
                                                {
                                                    sy = (int)main_scene.y0;
                                                }
                                                else
                                                {
                                                    if (main_scene.y0 >= panel3.Location.Y - P2.Size.Height - 5)
                                                    {
                                                        sy = panel3.Location.Y - P2.Size.Height - 5;
                                                    }
                                                }
                                                P2_GUI.Enabled = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (main_scene.wall == 4)
                                    {
                                        if (_y <= P2.Size.Height + P2.Location.Y && _y >= P2.Location.Y - Ball.Size.Height)
                                        {
                                            _x2 = _x - 10;
                                            _y2 = _x2 * _a + _b;
                                            _x1 = _x2;
                                            _y1 = 2 * _y - _y2;

                                            _a = (_y1 - _y) / (_x1 - _x);
                                            _b = _y1 - (_a * _x1);
                                            main_scene.y0 = panel2.Size.Height;
                                            main_scene.x0 = (main_scene.y0 - _b) / _a;
                                            if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                                            {
                                                main_scene.y0 = panel3.Location.Y - Ball.Size.Height;
                                                main_scene.x0 = (main_scene.y0 -_b) / _a;
                                                if (main_scene.x0 < P1.Location.X + P1.Size.Width || main_scene.x0 > P2.Location.X - Ball.Size.Width)
                                                {
                                                    main_scene.x0 = P1.Location.X + P1.Size.Width;
                                                    main_scene.y0 = _a * main_scene.x0 + _b;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                if (main_scene.regulate == 10)
                {
                    main_scene.speed++;
                    main_scene.regulate = 0;
                }
                main_scene.accept = 0;
            }

        }
    

    

        private void refresh()
        {
            Ball.Location = new Point(panel2.Size.Width / 2 - 5, panel3.Location.Y / 2 - panel2.Size.Height);
            main_scene.x = Ball.Location.X;
            main_scene.y = Ball.Location.Y;
            main_scene.x1 = 0;
            main_scene.y1 = 0;
            int ok = 0;
            while (ok == 0)
            {
                main_scene.wall = rand.Next(1, 4);
                if (main_scene.wall == 1)
                {
                    if (main_scene.part == 1)
                    {
                        main_scene.y1 = panel2.Size.Height;
                        main_scene.x1 = rand.Next(P1.Size.Width + P1.Location.X, panel2.Size.Width / 2 - 100);
                    }
                    else
                    {
                        if (main_scene.part == 2)
                        {
                            main_scene.y1 = panel2.Size.Height;
                            main_scene.x1 = rand.Next(panel2.Size.Width / 2 + 100, P2.Location.X - Ball.Size.Width + 1);
                        }
                    }
                }
                if (main_scene.wall == 2)
                {
                    if (main_scene.part == 1)
                    {
                        main_scene.x1 = P1.Size.Width + P1.Location.X;
                        main_scene.y1 = rand.Next(panel2.Size.Height, panel3.Location.Y + 1 - Ball.Size.Height);
                    }
                    else
                    {
                        if (main_scene.part == 2)
                        {
                            main_scene.x1 = P2.Location.X - Ball.Size.Width;
                            main_scene.y1 = rand.Next(panel2.Size.Height, panel3.Location.Y + 1 - Ball.Size.Height);
                            main_scene.wall = 4;
                        }
                    }
                }
                if (main_scene.wall == 3)
                {
                    if (main_scene.part == 1)
                    {
                        main_scene.y1 = panel3.Location.Y - Ball.Size.Height;
                        main_scene.x1 = rand.Next(P1.Size.Width + P1.Location.X, panel2.Size.Width / 2 - 100);
                    }
                    else
                    {
                        if (main_scene.part == 2)
                        {
                            main_scene.y1 = panel3.Location.Y - Ball.Size.Height;
                            main_scene.x1 = rand.Next(panel2.Size.Width / 2 + 100, P2.Location.X - Ball.Size.Width + 1);
                        }
                    }

                }
                
                if (main_scene.x1 != main_scene.x && main_scene.y1 != main_scene.y)
                {
                    main_scene.a = (main_scene.y1 - main_scene.y) / (main_scene.x1 - main_scene.x);
                    main_scene.b = main_scene.y1 - (main_scene.a * main_scene.x1);
                    main_scene.speed = 5;
                    ok = 1;
                    main_scene.accept = 0;
                    main_scene.regulate = 0;
                    main_scene.again = 0;
                    if(_command==1)
                    {
                        if (main_scene.part == 2)
                        {

                            if (main_scene.y1 < panel3.Location.Y - P2.Size.Height - 5)
                            {
                                sy = (int)main_scene.y1;
                            }
                            else
                            {
                                if (main_scene.y1 >= panel3.Location.Y - P2.Size.Height - 5)
                                {
                                    sy = panel3.Location.Y - P2.Size.Height - 5;
                                }
                            }
                            P2_GUI.Enabled = true;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            //Form2.Close();
        }
        int sy;
        private void P2_GUI_Tick(object sender, EventArgs e)
        {
            if (Level.Text != "ХАРД")
            {
                if (P2.Location.Y < sy)
                {
                    if (P2.Location.Y + pspeed < sy)
                    {
                        P2.Location = new Point(P2.Location.X, P2.Location.Y + pspeed);
                    }
                    else
                    {
                        P2.Location = new Point(P2.Location.X, sy);
                        P2_GUI.Enabled = false;
                    }
                }
                else
                {
                    if (P2.Location.Y > sy)
                    {
                        if (P2.Location.Y - pspeed > sy)
                        {
                            P2.Location = new Point(P2.Location.X, P2.Location.Y - pspeed);
                        }
                        else
                        {
                            P2.Location = new Point(P2.Location.X, sy);
                            P2_GUI.Enabled = false;
                        }

                    }
                    else
                    {
                        if (P2.Location.Y == sy)
                        {
                            P2.Location = new Point(P2.Location.X, sy);
                            P2_GUI.Enabled = false;
                        }
                    }
                }
            }

                

                

            
        }
        int pspeed = 1;
        private void Level_Click(object sender, EventArgs e)
        {
            if (_command == 1)
            {
                if (Level.Text == "ЛЕГКИЙ")
                {
                    Level.Text = "ЗВИЧАЙНИЙ";
                    pspeed = 5;
                }
                else
                {
                    if (Level.Text == "ЗВИЧАЙНИЙ")
                    {
                        Level.Text = "ВАЖКИЙ";
                        pspeed = 10;
                    }
                    else
                    {
                        if (Level.Text == "ВАЖКИЙ")
                        {
                            Level.Text = "ХАРД";
                        }
                        else
                        {
                            if (Level.Text == "ХАРД")
                            {
                                Level.Text = "ЛЕГКИЙ";
                                pspeed = 1;
                            }
                        }
                    }
                }
                main_scene.first++;
                if (main_scene.first == 1)
                {
                    main_scene.gx = Score.Location.X;
                    main_scene.gy = Score.Location.Y;
                }
                
                Score.Location = new Point(main_scene.gx, main_scene.gy);
                Score.Text = "0 : 0";
                main_scene.p1_score = 0;
                main_scene.p2_score = 0;
                Ball.Location = new Point(panel2.Size.Width / 2 - 5, panel3.Location.Y / 2 - panel2.Size.Height);
                main_scene.x = Ball.Location.X;
                main_scene.y = Ball.Location.Y;
                main_scene.x1 = 0;
                main_scene.y1 = 0;
                int ok = 0;
                while (ok == 0)
                {
                    main_scene.wall = rand.Next(1, 4);
                    if (main_scene.wall == 1)
                    {
                        main_scene.y1 = panel2.Size.Height;
                        main_scene.x1 = rand.Next(P1.Size.Width + P1.Location.X, panel2.Size.Width / 2 - 100);
                    }
                    if (main_scene.wall == 2)
                    {
                        main_scene.x1 = P1.Size.Width + P1.Location.X;
                        main_scene.y1 = rand.Next(panel2.Size.Height, panel3.Location.Y + 1 - Ball.Size.Height);
                    }
                    if (main_scene.wall == 3)
                    {
                        main_scene.y1 = panel3.Location.Y - Ball.Size.Height;
                        main_scene.x1 = rand.Next(P1.Size.Width + P1.Location.X, panel2.Size.Width / 2 - 100);
                    }

                    if (main_scene.x1 != main_scene.x && main_scene.y1 != main_scene.y)
                    {
                        main_scene.a = (main_scene.y1 - main_scene.y) / (main_scene.x1 - main_scene.x);
                        main_scene.b = main_scene.y1 - (main_scene.a * main_scene.x1);
                        main_scene.speed = 5;
                        P2_GUI.Enabled = false;
                        Ball_Move.Enabled = true;
                        ok = 1;
                        main_scene.accept = 0;
                        main_scene.regulate = 0;
                        main_scene.again = 0;
                    }
                }
            }
            

        }
        int _command = 1;
        string msg = "Pink: W-вгору , S-вниз" + '\n' + "Blue: O-вгору , L-вниз";

        private void Player_Command_Click(object sender, EventArgs e)
        {
            if (_command == 1)
            {
                Player_Command.Text = "МУЛЬТИПЛЕЙ";
                MessageBox.Show(msg);
                _command = 2;
                C1.Checked = false;
                C2.Checked = false;
                mouse = 0;
                C2.Text = "ВКЛ МИШКУ";
            }
            else
            {
                if (_command == 2)
                {
                    Player_Command.Text = "ОДИН ГРАВЕЦЬ";
                    _command = 1;
                    C1.Checked = true;
                    C2.Checked = false;
                    mouse = 1;
                    C2.Text = "";
                }
            }
            main_scene.first++;
            if (main_scene.first == 1)
            {
                main_scene.gx = Score.Location.X;
                main_scene.gy = Score.Location.Y;
            }
          
            Score.Location = new Point(main_scene.gx, main_scene.gy);
            Score.Text = "0 : 0";
            main_scene.p1_score = 0;
            main_scene.p2_score = 0;
            Ball.Location = new Point(panel2.Size.Width / 2 - 5, panel3.Location.Y / 2 - panel2.Size.Height);
            main_scene.x = Ball.Location.X;
            main_scene.y = Ball.Location.Y;
            main_scene.x1 = 0;
            main_scene.y1 = 0;
            int ok = 0;
            while (ok == 0)
            {
                main_scene.wall = rand.Next(1, 4);
                if (main_scene.wall == 1)
                {
                    main_scene.y1 = panel2.Size.Height;
                    main_scene.x1 = rand.Next(P1.Size.Width + P1.Location.X, panel2.Size.Width / 2 - 100);
                }
                if (main_scene.wall == 2)
                {
                    main_scene.x1 = P1.Size.Width + P1.Location.X;
                    main_scene.y1 = rand.Next(panel2.Size.Height, panel3.Location.Y + 1 - Ball.Size.Height);
                }
                if (main_scene.wall == 3)
                {
                    main_scene.y1 = panel3.Location.Y - Ball.Size.Height;
                    main_scene.x1 = rand.Next(P1.Size.Width + P1.Location.X, panel2.Size.Width / 2 - 100);
                }

                if (main_scene.x1 != main_scene.x && main_scene.y1 != main_scene.y)
                {
                    main_scene.a = (main_scene.y1 - main_scene.y) / (main_scene.x1 - main_scene.x);
                    main_scene.b = main_scene.y1 - (main_scene.a * main_scene.x1);
                    main_scene.speed = 5;
                    P2_GUI.Enabled = false;
                    Ball_Move.Enabled = true;
                    ok = 1;
                    main_scene.accept = 0;
                    main_scene.regulate = 0;
                    main_scene.again = 0;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((mouse == 0 && _command == 1) || mouse == 2 || (mouse == 0 && _command == 2))
            {
                if (e.KeyCode == Keys.W)
                {
                    p1_move = 1;
                    _p1tm.Enabled = true;
                }
                else
                {

                    if (e.KeyCode == Keys.S)
                    {
                        p1_move = 2;
                        _p1tm.Enabled = true;
                    }
                }
            }
            if ((mouse == 1 || mouse == 0) && _command == 2)
            {
                if (e.KeyCode == Keys.O)
                {
                    p2_move = 1;
                    _p2tm.Enabled = true;
                }
                else
                {

                    if (e.KeyCode == Keys.L)
                    {
                        p2_move = 2;
                        _p2tm.Enabled = true;
                    }
                }
            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.S)
            {
                p1_move = 0;
            }
            if (e.KeyCode == Keys.O || e.KeyCode == Keys.L)
            {
                p2_move = 0;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        int p1_move=0,p2_move=0;

        private void _p1tm_Tick(object sender, EventArgs e)
        {
            if (p1_move != 0)
            {
                if (p1_move == 1)
                {
                    if (P1.Location.Y - 10 >= panel2.Size.Height + 5)
                    {
                        P1.Location = new Point(P1.Location.X, P1.Location.Y - 10);
                    }
                    else
                    {
                        P1.Location = new Point(P1.Location.X, panel2.Size.Height + 5);
                    }
                }
                else
                {
                    if (p1_move == 2)
                    {
                        if (P1.Location.Y + 10 <= panel3.Location.Y - 5 - P1.Size.Height)
                        {
                            P1.Location = new Point(P1.Location.X, P1.Location.Y + 10);
                        }
                        else
                        {
                            P1.Location = new Point(P1.Location.X, panel3.Location.Y - 5 - P1.Size.Height);
                        }
                    }
                }
            }
            else
            {
                _p1tm.Enabled = false;
            }
         
            
        }

        private void _p2tm_Tick(object sender, EventArgs e)
        {
            if (p2_move != 0)
            {
                if (p2_move == 1)
                {
                    if (P2.Location.Y - 10 >= panel2.Size.Height + 5)
                    {
                        P2.Location = new Point(P2.Location.X, P2.Location.Y - 10);
                    }
                    else
                    {
                        P2.Location = new Point(P2.Location.X, panel2.Size.Height + 5);
                    }
                }
                else
                {
                    if (p2_move == 2)
                    {
                        if (P2.Location.Y + 10 <= panel3.Location.Y - 5 - P2.Size.Height)
                        {
                            P2.Location = new Point(P2.Location.X, P2.Location.Y + 10);
                        }
                        else
                        {
                            P2.Location = new Point(P2.Location.X, panel3.Location.Y - 5 - P2.Size.Height);
                        }
                    }
                }
            }
            else
            {
                _p2tm.Enabled = false;
            }
        }

        private void C2_KeyDown(object sender, KeyEventArgs e)
        {
            if ((mouse == 0 && _command == 1) || mouse == 2 || (mouse == 0 && _command == 2))
            {
                if (e.KeyCode == Keys.W)
                {
                    p1_move = 1;
                    _p1tm.Enabled = true;
                }
                else
                {

                    if (e.KeyCode == Keys.S)
                    {
                        p1_move = 2;
                        _p1tm.Enabled = true;
                    }
                }
            }
            if ((mouse == 1 || mouse == 0) && _command == 2)
            {
                if (e.KeyCode == Keys.O)
                {
                    p2_move = 1;
                    _p2tm.Enabled = true;
                }
                else
                {

                    if (e.KeyCode == Keys.L)
                    {
                        p2_move = 2;
                        _p2tm.Enabled = true;
                    }
                }
            }
        }

        private void C2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.S)
            {
                p1_move = 0;
            }
            if (e.KeyCode == Keys.O || e.KeyCode == Keys.L)
            {
                p2_move = 0;
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void C1_CheckedChanged(object sender, EventArgs e)
        {
            if (_command == 1)
            {
                if (C1.Checked == false)
                {
                    C2.Checked = true;
                    mouse = 0;
                }
                else
                {
                    if (C1.Checked == true)
                    {
                        C2.Checked = false;
                        mouse = 1;
                    }
                }
            }
            else
            {
                if (_command == 2)
                {
                    if (C1.Checked == true)
                    {
                        if (C2.Checked == false)
                        {
                            mouse = 1;
                        }
                        else
                        {
                            C1.Checked = false;
                        }
                    }
                    else
                    {
                        if (C2.Checked == false)
                        {
                            mouse = 0;
                        }
                        else
                        {
                            mouse = 2; ;
                        }
                    }
                }

            }
        }
        int mouse = 1;

        private void P1_Paint(object sender, PaintEventArgs e)
        { 
        }

        private void C2_CheckedChanged(object sender, EventArgs e)
        {
            if (_command == 1)
            {
                if (C2.Checked == false)
                {
                    C1.Checked = true;
                    mouse = 1;
                }
                else
                {
                    if (C2.Checked == true)
                    {
                        C1.Checked = false;
                        mouse = 0;
                    }
                }
            }
            else
            {
                if (_command == 2)
                {
                    if (C2.Checked == true)
                    {
                        if (C1.Checked == false)
                        {
                            mouse = 2;
                        }
                        else
                        {
                            C2.Checked = false;
                        }
                    }
                    else
                    {
                        if (C1.Checked == false)
                        {
                            mouse = 0;
                        }
                        else
                        {
                            mouse = 1; ;
                        }
                    }
                }
                
            }
        }

        

        



        

       
       



      
    }
}
