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
            MoveableObjects.Add(new Meteoroid(new Point(1000, 210), 1, VectorUtils.GetVector(0.8, 180), 50, 75));
            MoveableObjects.Add(new Meteoroid(new Point(500, 310), 1, VectorUtils.GetVector(0.8, 180), 50, 75));
            MoveableObjects.Add(new Meteoroid(new Point(700, 010), 1, VectorUtils.GetVector(0.8, 180), 50, 75));
            MoveableObjects.Add(new Rocket(new Point(200, 200), 50, 89));

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
            Rocket = new Rocket(new Point(200, 200), 50, 89);
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
                    Vector xAxis = VectorUtils.GetVector(1, 0);
                    Vector navigatorCenter = new Vector(50, 50);
                    Vector rocketPosition = Vector.Subtract(new Vector(Rocket.Position.X, Rocket.Position.Y), navigatorCenter);
                    Vector navigatorXAxis = Vector.Subtract(new Vector(100, 50), navigatorCenter);
                    Rocket.PositionAngle = Vector.AngleBetween(rocketPosition, navigatorXAxis);

                    moveableObject.ViewDirectionAngle += moveableObject.RotationSpeed;
                    moveableObject.ThrustMovement = VectorUtils.GetVector(moveableObject.Thrust, moveableObject.ViewDirectionAngle);

                    moveableObject.FlightDirectionAngle = 360 - Vector.AngleBetween(moveableObject.Velocity, xAxis);

                    moveableObject.Velocity = Vector.Add(moveableObject.Velocity, moveableObject.ThrustMovement);
                }

                for (int i = 0; i < MoveableObjects.Count; i++)
                {
                    IMoveableObject objectA = MoveableObjects[i];
                    IEnumerable<IMoveableObject> remainingObjects = MoveableObjects.Skip(i + 1);
                    foreach (IMoveableObject objectB in remainingObjects)
                    {
                        double distance = VectorUtils.GetDistance(objectA, objectB);

                        if (distance < objectA.CollisionRadius + objectB.CollisionRadius)
                        {
                            Vector resultingVelocityA = GetResultingVelocityFromCollision(objectA, objectB);
                            Vector resultingVelocityB = GetResultingVelocityFromCollision(objectB, objectA);
                            objectA.Velocity = resultingVelocityA;
                            objectB.Velocity = resultingVelocityB;
                        }
                    }
                }

                UpdateObjects();

                Thread.Sleep(30);
            }
        }

        private Vector GetResultingVelocityFromCollision(IMoveableObject o1, IMoveableObject o2)
        {
            double mass = (2 * o2.Mass) / (o1.Mass + o2.Mass);
            double magnitude = Math.Pow(Vector.Subtract(o2.Center, o1.Center).Length, 2);
            Vector distance = Vector.Subtract(o1.Center, o2.Center);
            double dotProduct = Vector.Multiply(
                                                Vector.Subtract(o1.Velocity, o2.Velocity),
                                                distance);
            Vector productTerm2 = Vector.Multiply(mass * dotProduct / magnitude, distance);
            Vector result = Vector.Subtract(o1.Velocity, productTerm2);
            return result;
        }

        private void UpdateObjects()
        {
            foreach (IMoveableObject moveableObject in MoveableObjects)
            {
                moveableObject.Position = new Point
                    {
                        X = moveableObject.Position.X + moveableObject.Velocity.X,
                        Y = moveableObject.Position.Y - moveableObject.Velocity.Y
                    };
                moveableObject.Update();
            }
        }
    }
}