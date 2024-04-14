using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LuigiBoardController : MonoBehaviour
{
    [SerializeField]
    private GameObject lettersCollection;

    [SerializeField]
    private GameObject marker;

    public AudioSource audioSource;

    public AudioClip returnSfx;

    public AudioClip spaceSfx;

    public AudioClip letterSfx1;
    public AudioClip letterSfx2;
    public AudioClip letterSfx3;
    public AudioClip letterSfx4;
    public AudioClip letterSfx5;

    private List<AudioClip> letterSfxs;

    internal Vector3 markerDefaultPosition {get; set;}

    private Dictionary<char, Vector3> letterPositionMap {get; set;} = new Dictionary<char, Vector3>();

    private Dictionary<KeyCode, char> keysToCheckMap {get; set;}

    public DefaultState defaultState;

    public OnLetterState onLetterState;

    private IState currentState;

    public List<char> charBuffer {get; set;} = new List<char>(64);

    internal List<SinnerDataModel> sinners {get; set;}

    internal SinnerDataModel currentSinner {get; set;}

    public void SetState(IState state) {
        currentState.OnExit();
        currentState = state;
        currentState.OnEnter();
    }

    public Vector3 GetMarkerPosition() {
        return marker.transform.position;
    }

    public void SetMarkerPosition(Vector3 position) {
        marker.transform.position = position;
    }

    public AudioClip RandomLetterSfx() {
        return letterSfxs[Random.Range(0, letterSfxs.Count - 1)];
    }

    void OnEnable() {
        markerDefaultPosition = marker.transform.position;
        defaultState = new DefaultState(this, markerDefaultPosition);
        onLetterState = new OnLetterState(this);
        currentState = defaultState;

        var letters = lettersCollection.transform.GetComponentsInChildren<Transform>();
        // Hack: Skip the first one because it's the collection object.
        foreach(var child in letters.TakeLast(letters.Length - 1)) {
            letterPositionMap[char.Parse(child.gameObject.name)] = new Vector3(child.position.x,
                                                                               child.position.y,
                                                                               child.position.z);
        }

        sinners = SinnerDataModel.LoadSinnersFromJson();
        currentSinner = sinners.First();

        letterSfxs = new List<AudioClip>{
            letterSfx1,
            letterSfx2,
            letterSfx3,
            letterSfx4,
            letterSfx5,
        };

        keysToCheckMap = new Dictionary<KeyCode, char> {
            {KeyCode.A, 'A'},
            {KeyCode.B, 'B'},
            {KeyCode.C, 'C'},
            {KeyCode.D, 'D'},
            {KeyCode.E, 'E'},
            {KeyCode.F, 'F'},
            {KeyCode.G, 'G'},
            {KeyCode.H, 'H'},
            {KeyCode.I, 'I'},
            {KeyCode.J, 'J'},
            {KeyCode.K, 'K'},
            {KeyCode.L, 'L'},
            {KeyCode.M, 'M'},
            {KeyCode.N, 'N'},
            {KeyCode.O, 'O'},
            {KeyCode.P, 'P'},
            {KeyCode.Q, 'Q'},
            {KeyCode.R, 'R'},
            {KeyCode.S, 'S'},
            {KeyCode.T, 'T'},
            {KeyCode.U, 'U'},
            {KeyCode.V, 'V'},
            {KeyCode.W, 'W'},
            {KeyCode.X, 'X'},
            {KeyCode.Y, 'Y'},
            {KeyCode.Z, 'Z'},
            {KeyCode.Alpha1, '1'},
            {KeyCode.Alpha2, '2'},
            {KeyCode.Alpha3, '3'},
            {KeyCode.Alpha4, '4'},
            {KeyCode.Alpha5, '5'},
            {KeyCode.Alpha6, '6'},
            {KeyCode.Alpha7, '7'},
            {KeyCode.Alpha8, '8'},
            {KeyCode.Alpha9, '9'},
            {KeyCode.Alpha0, '0'},
            {KeyCode.Space, ' '},
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) {
            foreach (var key in keysToCheckMap) {
                if (Input.GetKeyDown(key.Key)) {
                    var keyName = key.Value;
                    var letterPosition = letterPositionMap[keyName];

                    charBuffer.Add(keyName);

                    if (currentState == defaultState || currentState == onLetterState) {
                        SetState(new TransitionState(this, keyName, marker.transform.position, letterPosition, onLetterState));
                    }
                }
            }
        }

        if (currentState != null) {
            currentState.Update();
        }
    }
}
