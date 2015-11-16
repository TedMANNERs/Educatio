using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TheNewEra
{
    public class Universe : IUniverse
    {
        private bool _isRunning = true;

        public Universe(KeyboardListener keyboardListener)
        {
            KeyboardListener = keyboardListener;

            MoveableObjects = new ObservableCollection<IMoveableObject>();
            MoveableObjects.Add(new Meteoroid(1000, 200, 1, VectorUtils.GetScaledVector(8, 180), 50, 75));
            MoveableObjects.Add(new Meteoroid(800, 150, -2, VectorUtils.GetScaledVector(7, 178), 66, 95));
            MoveableObjects.Add(new Rocket(200, 200, 50, 89));

            Rocket = MoveableObjects.OfType<Rocket>().Single();
            Rocket.FuelTankSize = 5000;
            Rocket.RemainingFuel = 5000;

            KeyboardListener.Subscribers.Add(Key.W, Rocket.IncreaseThrust);
            KeyboardListener.Subscribers.Add(Key.A, Rocket.RotateLeft);
            KeyboardListener.Subscribers.Add(Key.S, Rocket.DecreaseThrust);
            KeyboardListener.Subscribers.Add(Key.D, Rocket.RotateRight);
            KeyboardListener.Subscribers.Add(Key.R, Rocket.PressedR);
            KeyboardListener.Subscribers.Add(Key.T, Rocket.PressedT);
            KeyboardListener.Start();

            Task task = new Task(Loop);
            task.Start();
        }

        public Universe()
        {
            Rocket = new Rocket(200, 200, 50, 89);
            Rocket.FuelTankSize = 500;
            Rocket.RemainingFuel = 500;
        }

        public ObservableCollection<IMoveableObject> MoveableObjects { get; set; }
        public Rocket Rocket { get; set; }
        public KeyboardListener KeyboardListener { get; private set; }

        private void Loop()
        {
            while (_isRunning)
            {
                foreach (IMoveableObject moveableObject in MoveableObjects)
                {
                    Vector xAxis = VectorUtils.GetScaledVector(1, 0);
                    Vector navigatorCenter = new Vector(50, 50);
                    Vector rocketPosition = Vector.Subtract(new Vector(Rocket.X, Rocket.Y), navigatorCenter);
                    Vector navigatorXAxis = Vector.Subtract(new Vector(100, 50), navigatorCenter);
                    Rocket.PositionAngle = Vector.AngleBetween(rocketPosition, navigatorXAxis);

                    moveableObject.ViewDirectionAngle += moveableObject.RotationThrust;
                    moveableObject.ThrustMovement = VectorUtils.GetScaledVector(moveableObject.Thrust, moveableObject.ViewDirectionAngle);

                    moveableObject.FlightDirectionAngle = 360 - Vector.AngleBetween(moveableObject.SpaceMovement, xAxis);

                    Point movement = VectorUtils.GetCoordinates(moveableObject.SpaceMovement.Length, -moveableObject.FlightDirectionAngle);
                    moveableObject.X += movement.X;
                    moveableObject.Y += movement.Y;
                    movement = VectorUtils.GetCoordinates(moveableObject.ThrustMovement.Length, moveableObject.ViewDirectionAngle);
                    moveableObject.X += movement.X;
                    moveableObject.Y += movement.Y;

                    moveableObject.SpaceMovement = Vector.Add(moveableObject.SpaceMovement, moveableObject.ThrustMovement);

                    moveableObject.Update();
                }

                Thread.Sleep(30);
            }
        }

        public void Stop()
        {
            _isRunning = false;
            KeyboardListener.Stop();
        }
    }
}