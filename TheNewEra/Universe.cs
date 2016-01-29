using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TheNewEra.KeyboardListener;
using TheNewEra.Objects;
using TheNewEra.Objects.Rocket;
using TheNewEra.Physics;

namespace TheNewEra
{
    public class Universe : IUniverse
    {
        private bool _isRunning = true;

        public Universe(IKeyboardListener keyboardListener, IPhysicsEngine physicsEngine)
        {
            KeyboardListener = keyboardListener;
            PhysicsEngine = physicsEngine;

            MoveableObjects = new ObservableCollection<IMoveableObject>
                {
                    new Meteoroid(new Vector(1400, 500), 15, 25, new Vector(0, -18), 1) { Mass = 100 },
                    new Meteoroid(new Vector(1500, 500), 15, 25, new Vector(0, -18), 1) { Mass = 100 },
                    new Meteoroid(new Vector(1350, 500), 15, 25, new Vector(0, -18), 1) { Mass = 100 },
                    new Meteoroid(new Vector(1300, 500), 15, 25, new Vector(0, -13), 1) { Mass = 50 },
                    new Meteoroid(new Vector(1000, 1000), 15, 25, new Vector(-10, 0), 1) { Mass = 50 },
                    new Meteoroid(new Vector(600, 500), 15, 25, new Vector(0, 9), 1) { Mass = 30 },
                    new Planet(new Vector(1000, 500), 200, 200) { Mass = 2000000000000 },
                    new Rocket(new Vector(500, 500), 30, 55) { Mass = 100, FuelTank = new FuelTank(2000), Velocity = new Vector(0, 15) }
                };

            Rocket = MoveableObjects.OfType<Rocket>().Single();

            KeyboardListener.Subscribers.Add(new Input(Key.W, Rocket.IncreaseThrust));
            KeyboardListener.Subscribers.Add(new Input(Key.A, Rocket.RotateLeft));
            KeyboardListener.Subscribers.Add(new Input(Key.S, Rocket.DecreaseThrust));
            KeyboardListener.Subscribers.Add(new Input(Key.D, Rocket.RotateRight));
            KeyboardListener.Subscribers.Add(new Input(Key.R, Rocket.PressedR));
            KeyboardListener.Subscribers.Add(new Input(Key.T, Rocket.PressedT));
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
        public IKeyboardListener KeyboardListener { get; private set; }
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