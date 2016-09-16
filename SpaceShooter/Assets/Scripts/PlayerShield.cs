using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerShield : MonoBehaviour {

	public float m_StartingSheild = 100f;   
	public Color m_FullHealthColor = Color.green;       
	public Color m_ZeroHealthColor = Color.red;
	public AudioSource m_EnergyShieldAudio;

	private float m_CurrentSheild;
	private Slider m_shieldBar;
	private Image m_FillImage;

	private void OnEnable()
	{
		m_shieldBar = GameObject.FindGameObjectWithTag ("ShieldBar").GetComponent<Slider> ();
		m_CurrentSheild = m_StartingSheild;

		SetHealthUI();
	}


	public void TakeDamage (float amount)
	{
		m_CurrentSheild -= amount;
		m_EnergyShieldAudio.Play ();
		SetHealthUI();
	}

	private void SetHealthUI ()
	{
		// Set the slider's value appropriately.
		m_shieldBar.value = m_CurrentSheild;

			//m_FillImage.color = Color.Lerp (m_ZeroHealthColor, m_FullHealthColor, m_CurrentSheild / m_StartingSheild);
	}

	public float getCurrentSheild()
	{
		return m_CurrentSheild;
	}
}
