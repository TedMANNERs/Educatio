using System;
using System.Collections.Generic;
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

        public void Stop()
        {
            _isRunning = false;
            KeyboardListener.Stop();
        }

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

                    moveableObject.ViewDirectionAngle += moveableObject.RotationSpeed;
                    moveableObject.ThrustMovement = VectorUtils.GetScaledVector(moveableObject.Thrust, moveableObject.ViewDirectionAngle);

                    moveableObject.FlightDirectionAngle = 360 - Vector.AngleBetween(moveableObject.SpaceMovement, xAxis);

                    moveableObject.SpaceMovement = Vector.Add(moveableObject.SpaceMovement, moveableObject.ThrustMovement);
                }

                for (int i = 0; i < MoveableObjects.Count; i++)
                {
                    IMoveableObject objectA = MoveableObjects[i];
                    IEnumerable<IMoveableObject> remainingObjects = MoveableObjects.Skip(i + 1);
                    foreach (IMoveableObject objectB in remainingObjects)
                    {
                        Point centerA = new Point(objectA.X + objectA.CenterX, objectA.Y + objectA.CenterY);
                        Point centerB = new Point(objectB.X + objectB.CenterX, objectB.Y + objectB.CenterY);

                        double distanceX = centerA.X - centerB.X;
                        double distanceY = centerA.Y - centerB.Y;
                        double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);

                        if (distance < objectA.CollisionRadius + objectB.CollisionRadius)
                        {
                            double velocityX = objectB.SpaceMovement.X - objectA.SpaceMovement.X;
                            double velocityY = objectB.SpaceMovement.Y - objectA.SpaceMovement.Y;
                            double dotProduct = distanceX * velocityX + distanceY * velocityY;

                            if (dotProduct > 0)
                            {
                            }
                        }
                    }
                }

                UpdateObjects();

                Thread.Sleep(30);
            }
        }

        private void UpdateObjects()
        {
            foreach (IMoveableObject moveableObject in MoveableObjects)
            {
                moveableObject.X += moveableObject.SpaceMovement.X;
                moveableObject.Y -= moveableObject.SpaceMovement.Y;
                moveableObject.Update();
            }
        }
    }
}