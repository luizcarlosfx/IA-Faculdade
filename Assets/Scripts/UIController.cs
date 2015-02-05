using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Robot Robot;

    public InputField RobotSpeedInput;

    public Toggle SmartGoalToggle;

    private static bool _smart;
    private static float _speed;
    private static bool _initialized;

    void Start()
    {
        if (!_initialized)
        {
            _initialized = true;
            _smart = SmartGoalToggle.isOn;
            _speed = float.Parse(RobotSpeedInput.text);
        }
        else
        {
            SmartGoalToggle.isOn = _smart;
            RobotSpeedInput.text = _speed + string.Empty;
        }
        ToggleAlgorithm();
        ChangeSpeed();
    }

    public void ToggleAlgorithm()
    {
        Robot.UseSmartGoal = SmartGoalToggle.isOn;
        _smart = SmartGoalToggle.isOn;
    }

    public void ChangeSpeed()
    {
        float.TryParse(RobotSpeedInput.text, out Robot.MoveSpeed);
        _speed = Robot.MoveSpeed;
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
