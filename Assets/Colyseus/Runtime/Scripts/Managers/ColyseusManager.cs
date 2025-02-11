using System.Collections.Generic;
using LucidSightTools;
using UnityEngine;
// ReSharper disable InconsistentNaming

namespace Colyseus
{
    /// <summary>
    /// Base manager class 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ColyseusManager<T> : MonoBehaviour
    {
        /// <summary>
        /// Reference to the Colyseus settings object.
        /// </summary>
        [SerializeField]
        protected ColyseusSettings _colyseusSettings;

        private ColyseusRequest _requests;

        // Getters
        //==========================
        /// <summary>
        /// The singleton instance of the Colyseus Manager.
        /// </summary>
        public static T Instance { get; private set; }

        /// <summary>
        /// Returns the Colyseus server address as defined
        /// in the <see cref="ColyseusSettings"/> object
        /// </summary>
        public string ColyseusServerAddress
        {
            get { return _colyseusSettings.colyseusServerAddress; }
        }

        /// <summary>
        /// Returns the Colyseus server port as defined
        /// in the <see cref="ColyseusSettings"/> object
        /// </summary>
        public string ColyseusServerPort
        {
            get { return _colyseusSettings.colyseusServerPort; }
        }

        /// <summary>
        /// Returned if the desired protocol security as defined
        /// in the <see cref="ColyseusSettings"/> object
        /// </summary>
        public bool ColyseusUseSecure
        {
            get { return _colyseusSettings.useSecureProtocol; }
        }
        //==========================

        /// <summary>
        /// The Client that is created when connecting to the Colyseus server.
        /// </summary>
        protected ColyseusClient client;

        /// <summary>
        /// <see cref="MonoBehaviour"/> callback when the manager object has been destroyed.
        /// </summary>
        protected virtual void OnDestroy()
        {
        }

        /// <summary>
        /// <see cref="MonoBehaviour"/> callback when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            // Copy request headers
            List<ColyseusSettings.RequestHeader> requestHeaders =
                new List<ColyseusSettings.RequestHeader>(_colyseusSettings.GetRequestHeaders());

            InitializeInstance();
        }

        /// <summary>
        /// Initializes the Colyseus manager singleton.
        /// </summary>
        private void InitializeInstance()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = GetComponent<T>();

            // Initialize the requests object with settings
            _requests = new ColyseusRequest(_colyseusSettings);
        }

        /// <summary>
        /// <see cref="MonoBehaviour"/> callback when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        protected virtual void Start()
        {
        }

        /// <summary>
        /// Frame-rate independent message for physics calculations.
        /// </summary>
        protected virtual void FixedUpdate()
        {
        }

        /// <summary>
        /// Override the current <see cref="ColyseusSettings"/>
        /// </summary>
        /// <param name="newSettings">The new settings to use</param>
        public virtual void OverrideSettings(ColyseusSettings newSettings)
        {
            _colyseusSettings = newSettings;
        }

        /// <summary>
        /// Get a copy of the manager's settings configuration
        /// </summary>
        /// <returns></returns>
        public virtual ColyseusSettings CloneSettings()
        {
            return ColyseusSettings.Clone(_colyseusSettings);
        }

        /// <summary>
        /// Creates a new <see cref="ColyseusClient"/> object, with the given endpoint, and returns it
        /// </summary>
        /// <param name="endpoint">URL to the Colyseus server</param>
        /// <returns></returns>
        public ColyseusClient CreateClient(string endpoint)
        {
            client = new ColyseusClient(endpoint);
            return client;
        }

        /// <summary>
        /// Connect to the Colyseus server.
        /// </summary>
        protected virtual void ConnectToServer()
        {
            CreateClient(_colyseusSettings.WebSocketEndpoint);
        }

        /// <summary>
        /// <see cref="MonoBehaviour"/> callback that gets called just before app exit.
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
        }
    }
}