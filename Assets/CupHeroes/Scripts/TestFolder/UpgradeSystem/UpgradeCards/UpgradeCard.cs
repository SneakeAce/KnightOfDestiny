using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : UIElement, IUpgradeableCard
{
    private Transform _parent;

    private Button _button;
    private Image _backgroundImage;
    private Outline _outline;

    private UpgradeCardConfig _config;

    private RareCardType _rareType;
    private UpgradeType _upgradeType;
    private float _multiplierValue;

    public UpgradeCardConfig Config => _config;
    public Transform Transform => transform;
    public Transform Parent => _parent;
    public Button ButtonSelectCard => _button;
    public RareCardType RareType => _rareType;
    public UpgradeType UpgradeType => _upgradeType;
    public float MultiplierValue => _multiplierValue;

    public void SetParent(Transform parent) => _parent = parent;

    public void SetConfig(UpgradeCardConfig config) => _config = config;

    public void SetRareType(RareCardType type) => _rareType = type;

    public void SetUpgradeType(UpgradeType type) => _upgradeType = type;

    public void SetMultiplierValue(float value) => _multiplierValue = value;

    public override void Initialize()
    {
        SetComponents();

        Debug.Log($"{this.ToString()} - Initialize - button = {_button}");
        Debug.Log($"{this.ToString()} - Initialize - backgroundImage = {_backgroundImage}");
        Debug.Log($"{this.ToString()} - Initialize - outline = {_outline}");
    }

    public void SetComponentsColor(Color mainUpgradeCardColor, Color rarityCardColor)
    {
        _backgroundImage.color = mainUpgradeCardColor;
        _outline.effectColor = rarityCardColor;
    }

    public float GetUpgradeValue()
    {
        float value = _config.UpgradeValue * _multiplierValue;

        return value;
    }

    private void SetComponents()
    {
        if (_button == null)
            _button = GetComponentInChildren<Button>();

        if (_backgroundImage == null)
            _backgroundImage = GetComponentInChildren<Image>();

        if (_outline == null)
            _outline = GetComponentInChildren<Outline>();
    }

    private void OnDisable()
    {
        if (_backgroundImage != null)
            _backgroundImage.color = Color.white;

        if ( _outline != null)
            _outline.effectColor = Color.white;

        _rareType = RareCardType.None;
    }

}
