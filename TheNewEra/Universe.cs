using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TheNewEra.Objects;
using TheNewEra.Objects.Rocket;
using TheNewEra.Physics;
using TheNewEra.Util;

namespace TheNewEra
{
    public class Universe : IUniverse
    {
        private bool _isRunning = true;

        public Universe(KeyboardListener.KeyboardListener keyboardListener, IPhysicsEngine physicsEngine)
        {
            KeyboardListener = keyboardListener;
            PhysicsEngine = physicsEngine;

            MoveableObjects = new ObservableCollection<IMoveableObject>
                {
                    new Meteoroid(new Vector(1400, 0), 15, 25, new Vector(0, -18), 1) { Mass = 100 },
                    new Meteoroid(new Vector(1300, 0), 15, 25, new Vector(0, -13), 1) { Mass = 50 },
                    new Meteoroid(new Vector(600, 0), 15, 25, new Vector(0, 9), 1) { Mass = 30 },
                    new Planet(new Vector(1000, 0), 200, 200) { Mass = 2000000000000 },
                    new Rocket(new Vector(500, 0), 30, 55) { Mass = 100, FuelTank = new FuelTank(2000), Velocity = new Vector(0, 15)}
                };

            Rocket = MoveableObjects.OfType<Rocket>().Single();

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
            Rocket = new Rocket(new Vector(200, 200), 50, 89) { FuelTank = new FuelTank(1000) };
        }

        public ObservableCollection<IMoveableObject> MoveableObjects { get; set; }
        public Rocket Rocket { get; set; }
        public KeyboardListener.KeyboardListener KeyboardListener { get; private set; }
        public IPhysicsEngine PhysicsEngine { get; set; }

        public void Stop()
        {
            _isRunning = false;
            KeyboardListener.Stop();
        }

        private void Loop()
        {
            while (_isRunning)
            {
                PhysicsEngine.ApplyForces(MoveableObjects);
                PhysicsEngine.HandleCollisions(MoveableObjects);
                UpdateObjects();

                Thread.Sleep(30);
            }
        }

        private void UpdateObjects()
        {
            foreach (IMoveableObject moveableObject in MoveableObjects)
            {
                moveableObject.Update();
            }
        }
    }
}