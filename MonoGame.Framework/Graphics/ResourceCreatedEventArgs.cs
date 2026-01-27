using System;

namespace Microsoft.Xna.Framework.Graphics
{
    /// <summary>
    /// Provides data for the <see cref="GraphicsDevice.ResourceCreated"/> event. This class cannot be inherited.
    /// </summary>
    [Obsolete("The event this class is used with is not in use.")]
    public sealed class ResourceCreatedEventArgs : EventArgs
    {
        /// <summary>
        /// The newly created resource object.
        /// </summary>
        public Object Resource { get; internal set; }
    }
}
