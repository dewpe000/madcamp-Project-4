using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ShootLaser : MonoBehaviourPun {
    PhotonView PV;
    LaserBeam beam;
    public string medium;
    public int intensity;
    public Color color;
    public bool isHakcerTransparent;
    public bool isPlayerTransparent;

    private Material material;
    private Material transparentMaterial;
     
    public ShootLaser(string medium) {
        this.medium = medium;
        
    }


	void Start() {
        PV = photonView;
        material = Resources.Load("Materials/Laser", typeof(Material)) as Material;
        transparentMaterial = Resources.Load("Materials/TransparentLaser", typeof(Material)) as Material;
    }

    void Update() {
        DestroyBeam();
        beam = MakeLaserBeam();
        beam.CastRay(beam.pos, beam.dir, beam.laser, medium);
        
    }

    public void DestroyBeam() {
        if (beam != null && beam.laserObj != null)
            Destroy(beam.laserObj);
    }

    public LaserBeam MakeLaserBeam() {
        bool isTransparent = GameManager.isHacker ? isHakcerTransparent : isPlayerTransparent;
        return new LaserBeam(gameObject.transform.position, gameObject.transform.right, 0.1f,
            color, isTransparent ? transparentMaterial : material, medium, intensity, this, false);
    }

    public LaserBeam MakeCloneLaserBeam() {
        Color color = Color.white;
        color.a = 0f;
        return new LaserBeam(gameObject.transform.position, gameObject.transform.right, 0.1f, color, material, medium, intensity, this, true);
    }

    public void SetLaserTransparent(bool isTransparent, bool isHacker) {
        PV.RPC("SetLaserTransparentPun", RpcTarget.Others, isTransparent, isHacker);
    }

    [PunRPC]
    private void SetLaserTransparentPun(bool isTransparent, bool isHacker) {
        if (isHacker)
            this.isHakcerTransparent= isTransparent;
        else
            this.isPlayerTransparent = isTransparent;

    }
}
