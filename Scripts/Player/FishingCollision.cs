using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            Player.instance.CancelFishing();
            if (!Player.instance.adSource.isPlaying)
                Player.instance.adSource.PlayOneShot(Player.instance.failedFishingRodCast);
        }
        else
        {
            if (other.tag != "Wall")
            {
                if (!Player.instance.adSource.isPlaying)
                    StartCoroutine(playAudioSequentially());
            }
        }
    }

    IEnumerator playAudioSequentially()
    {
        yield return null;
        Player.instance.StartFishing();

        //1.Loop through each AudioClip
        for (int i = 0; i < Player.instance.adClips.Length; i++)
        {
            //2.Assign current AudioClip to audiosource
            Player.instance.adSource.clip = Player.instance.adClips[i];

            //3.Play Audio
            Player.instance.adSource.Play();

            //4.Wait for it to finish playing
            while (Player.instance.adSource.isPlaying)
            {
                yield return null;
            }

            //5. Go back to #2 and play the next audio in the adClips array
        }
    }
}
