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
                    new Meteoroid(new Vector(800, 200), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 500 },
                    new Meteoroid(new Vector(600, 210), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 500 },
                    new Meteoroid(new Vector(500, 310), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 800 },
                    new Meteoroid(new Vector(500, 250), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 600 },
                    new Meteoroid(new Vector(400, 160), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 800 },
                    new Meteoroid(new Vector(700, 150), 50, 75, VectorUtils.GetVector(0.8, Math.PI), 1) { Mass = 400 },
                    new Rocket(new Vector(200, 200), 50, 89) { Mass = 200, FuelTank = new FuelTank(1000) }
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