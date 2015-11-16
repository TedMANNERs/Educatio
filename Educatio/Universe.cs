using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Educatio
{
    public class Universe
    {
        private int _imageId = 1;

        public Universe(KeyboardListener keyboardListener)
        {
            KeyboardListener = keyboardListener;

            MoveableObjects = new ObservableCollection<IMoveableObject>();
            MoveableObjects.Add(new Meteoroid(1000, 200, 1, VectorUtils.GetVector(8, 180)));
            MoveableObjects.Add(new Meteoroid(800, 150, -2, VectorUtils.GetVector(7, 178)));
            MoveableObjects.Add(new Rocket(200, 200));

            Rocket = MoveableObjects.OfType<Rocket>().Single();
            Rocket.FuelTankSize = 500;
            Rocket.RemainingFuel = 500;

            KeyboardListener.Subscribers.Add(Key.W, Rocket.PressedW);
            KeyboardListener.Subscribers.Add(Key.A, Rocket.PressedA);
            KeyboardListener.Subscribers.Add(Key.S, Rocket.PressedS);
            KeyboardListener.Subscribers.Add(Key.D, Rocket.PressedD);
            KeyboardListener.Subscribers.Add(Key.R, Rocket.PressedR);
            KeyboardListener.Subscribers.Add(Key.T, Rocket.PressedT);
            KeyboardListener.Start();

            Task task = new Task(Loop);
            task.Start();
        }

        public Universe()
        {
            Rocket = new Rocket(200, 200);
            Rocket.FuelTankSize = 500;
            Rocket.RemainingFuel = 500;
        }

        public ObservableCollection<IMoveableObject> MoveableObjects { get; set; }
        public Rocket Rocket { get; set; }
        public KeyboardListener KeyboardListener { get; private set; }

        private void Loop()
        {
            while (MainViewModel.IsRunning)
            {
                foreach (IMoveableObject moveableObject in MoveableObjects)
                {
                    if (Rocket.RemainingFuel <= 0 || Rocket.AccelerationMovement.Length <= 0)
                    {
                        Rocket.Sprite = "Resources/Images/rocket3.png";
                    }
                    else
                    {
                        if (_imageId > 2)
                        {
                            _imageId = 1;
                        }
                        Rocket.Sprite = "Resources/Images/rocket" + _imageId + ".png";
                        _imageId++;
                    }

                    Vector xAxis = VectorUtils.GetVector(1, 0);
                    Vector navigatorCenter = new Vector(50, 50);
                    Vector rocketPosition = Vector.Subtract(new Vector(Rocket.X + 20, Rocket.Y - 10), navigatorCenter);
                    Vector navigatorXAxis = Vector.Subtract(new Vector(100, 50), navigatorCenter);
                    Rocket.PositionAngle = (int)Vector.AngleBetween(rocketPosition, navigatorXAxis);

                    moveableObject.ViewDirectionAngle += moveableObject.RotateAcceleration;
                    moveableObject.AccelerationMovement = VectorUtils.GetVector(moveableObject.Acceleration, moveableObject.ViewDirectionAngle);

                    moveableObject.FlightDirectionAngle = 360 - (int)Vector.AngleBetween(moveableObject.SpaceMovement, xAxis);

                    Point movement = VectorUtils.GetCoordinates(moveableObject.SpaceMovement.Length, -moveableObject.FlightDirectionAngle);
                    moveableObject.X += movement.X;
                    moveableObject.Y += movement.Y;
                    movement = VectorUtils.GetCoordinates(moveableObject.AccelerationMovement.Length, moveableObject.ViewDirectionAngle);
                    moveableObject.X += movement.X;
                    moveableObject.Y += movement.Y;

                    moveableObject.SpaceMovement = Vector.Add(moveableObject.SpaceMovement, moveableObject.AccelerationMovement);
                }

                Thread.Sleep(30);
            }
        }
    }
}