using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerShield : MonoBehaviour {

	public float m_StartingSheild = 100f;   
	public float invincibleTime = 3.0f;
	bool isInvincible = false;
	public Color m_FullHealthColor = Color.green;       
	public Color m_ZeroHealthColor = Color.red;
	public AudioSource m_EnergyShieldAudio;
	public Renderer rend;
	public GameObject engine;
	public GameObject m_ForceSheild;

	private float m_CurrentSheild;
	private Slider m_shieldBar;
	private Image m_FillImage;
	private ParticleSystem[] m_particle;

	private void OnEnable()
	{
		m_shieldBar = GameObject.FindGameObjectWithTag ("ShieldBar").GetComponent<Slider> ();
		m_ForceSheild.SetActive (false);
		m_CurrentSheild = m_StartingSheild;

		m_particle = engine.GetComponentsInChildren<ParticleSystem> ();

		SetInvincible ();

		SetHealthUI();
	}

	public void SetInvincible()
	{
		isInvincible = true;

		BlinkPlayer (invincibleTime);

		CancelInvoke ("SetDamageable"); // in case the method has already been invoked
		Invoke( "SetDamageable", invincibleTime );

		CancelInvoke ("onEngineParticle"); // in case the method has already been invoked
		Invoke( "onEngineParticle", invincibleTime );
	}

	void SetDamageable()
	{
		isInvincible = false;
	}

	public bool IsInvincible ()
	{
		return isInvincible;
	}

	void BlinkPlayer(float timesBlinks) {
		StartCoroutine(DoBlinks(timesBlinks, 0.2f));
	}

	IEnumerator DoBlinks(float timesBlinks, float seconds) 
	{
		float times = 0f;
		while (times < timesBlinks) 
		{
			//toggle renderer
			rend.enabled = !rend.enabled;

			//wait for a bit
			yield return new WaitForSeconds (seconds);
			times += seconds;
		}

		//make sure renderer is enabled when we exit
		rend.enabled = true;
	}

	void onEngineParticle()
	{
		foreach (ParticleSystem stuff in m_particle)
		{
			ParticleSystem.EmissionModule em = stuff.GetComponentInChildren<ParticleSystem>().emission;
			em.enabled = true;
		}
	}

	public void TakeDamage (float amount)
	{
		if (!isInvincible) 
		{
			m_ForceSheild.SetActive (true);
			m_CurrentSheild -= amount;
			m_EnergyShieldAudio.Play ();
			SetHealthUI ();

			CancelInvoke ("disShowSheild");
			Invoke ("disShowSheild", 0.5f);
		}
	}

	void disShowSheild ()
	{
		m_ForceSheild.SetActive (false);
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
