using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnIdle()
    {
        EventBus.Instance.Publish(GlobalEvent.AllIdle);
    }
    public void OnAttack()
    {
        EventBus.Instance.Publish(GlobalEvent.AllAttack);
    }
    public void OnSpin()
    {
        EventBus.Instance.Publish(GlobalEvent.AllSpin);
    }
    public void OnDeath()
    {
        EventBus.Instance.Publish(GlobalEvent.AllDeath);
    }
    public void OnRun()
    {
        EventBus.Instance.Publish(GlobalEvent.AllRun);
    }
    public void onSpawn()
    {
        SpawnManager.Instance.Spawn();
    }
    public void OnNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
