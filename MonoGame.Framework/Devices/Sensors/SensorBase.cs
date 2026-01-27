// MonoGame - Copyright (C) MonoGame Foundation, Inc
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework;
using System;

namespace MonoGame.Framework.Devices.Sensors
{

    /// <summary>
    /// Base class for all sensor devices the application can access.
    /// </summary>
    /// <typeparam name="TSensorReading">The object type containing the sensor's provided data</typeparam>
	public abstract class SensorBase<TSensorReading> : IDisposable
		where TSensorReading : ISensorReading
	{
        bool _disposed;
		private TimeSpan _timeBetweenUpdates;
	    private TSensorReading _currentValue;
 
        /// <summary>
        /// The current value of the sensor's reading.
        /// </summary>
		public TSensorReading CurrentValue 
        {
            get { return _currentValue; }
		    protected set
		    {
		        _currentValue = value;

                var handler = CurrentValueChanged;

                if (handler != null)
                { 
                    handler(this, new SensorReadingEventArgs<TSensorReading>(value));
                }
		    }
		}

        /// <summary>
        /// Whether the current sensor data is valid.
        /// </summary>
		public bool IsDataValid { get; protected set; }


        /// <summary>
        /// The timespan between the two most recent updates of the sensor's reading.
        /// </summary>
		public TimeSpan TimeBetweenUpdates
		{
			get { return this._timeBetweenUpdates; }
			set
			{
				if (this._timeBetweenUpdates != value)
				{
					this._timeBetweenUpdates = value;
					EventHelpers.Raise(this, TimeBetweenUpdatesChanged, EventArgs.Empty);
				}
			}
		}
        
        /// <summary>
        /// Invoked when the current sensor's reading value has changed.
        /// </summary>
		public event EventHandler<SensorReadingEventArgs<TSensorReading>> CurrentValueChanged;

        /// <summary>
        /// Internal event which is invoked when the <see cref="TimeBetweenUpdates"/> property changes.
        /// </summary>

        protected event EventHandler<EventArgs> TimeBetweenUpdatesChanged;

        /// <summary>
        /// Whether the object has been disposed.
        /// </summary>
        protected bool IsDisposed { get { return _disposed; } }

        /// <summary>
        /// Default constructor for the <see cref="SensorBase{TSensorReading}"/> type.
        /// </summary>
        protected SensorBase()
		{
			this.TimeBetweenUpdates = TimeSpan.FromMilliseconds(2);
		}


        /// <summary>
        /// Default destructor for the <see cref="SensorBase{TSensorReading}"/> type.
        /// </summary>
        ~SensorBase()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Derived classes override this method to dispose of managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">True if unmanaged resources are to be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            _disposed = true;
        }
        
        /// <summary>
        /// Starts data acquisition from the sensor.
        /// </summary>
        public abstract void Start();
        
        /// <summary>
        /// Stops data acquisition from the sensor.
        /// </summary>
        public abstract void Stop();
	}
}

