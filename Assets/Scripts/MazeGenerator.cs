using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {
    public GameObject wall;
    public GameObject floor;
    public GameObject goal;
    const int WallSize = 1;
    public const int StageWidth = 19;
    public const int StageHeight = 19;
    readonly int[] DX = { 0, -1, 0, 1 };
    readonly int[] DZ = { -1, 0, 1, 0 };

    bool[,] stage = new bool[StageWidth, StageHeight];
    int[] candidateX = new int[StageWidth * StageHeight]; // åÛï‚
    int[] candidateZ = new int[StageWidth * StageHeight]; // åÛï‚

    // åäå@ÇËñ@
    int Dig(int x, int z) {
        int c = 0; // ÉJÉEÉìÉ^
        int r = Random.Range(0, 4);

        while (c < 4) {
            // å@ÇÍÇÈÇ©
            if (x + DX[r] * 2 <= 0 || z + DZ[r] * 2 <= 0 || x + DX[r] * 2 >= StageWidth - 1 || z + DZ[r] * 2 >= StageHeight - 1 || !stage[x + DX[r] * 2, z + DZ[r] * 2]) c++;
            else if (stage[x + DX[r] * 2, z + DZ[r] * 2]) {
                // å@ÇÍÇÈÇÃÇ≈å@ÇÈ (1Ç¬êÊÇ∆2Ç¬êÊÇÇ¬Ç»ÇÆ)
                stage[x + DX[r], z + DZ[r]] = false;
                stage[x + DX[r] * 2, z + DZ[r] * 2] = false;
                x = x + DX[r] * 2;
                z = z + DZ[r] * 2;
                c = 0;
                r = Random.Range(0, 4);
            }
        }

        return 0;
    }

    void initStage() {

        for (int x = 0; x < StageWidth; x++) for (int z = 0; z < StageHeight; z++) stage[x, z] = true;
        stage[1, 1] = false;
        while (true) {
            int i = 0;
            for (int x = 1; x < StageWidth - 1; x += 2) {
                for (int z = 1; z < StageHeight - 1; z += 2) {
                    if (!stage[x, z]) {
                        if (x - 2 >= 0 && stage[x - 2, z]) {
                            candidateX[i] = x;
                            candidateZ[i] = z;
                            ++i;
                        }
                        else if (z - 2 >= 0 && stage[x, z - 2]) {
                            candidateX[i] = x;
                            candidateZ[i] = z;
                            ++i;
                        }
                        else if (x == StageWidth - 2 && z == StageHeight - 2) break;
                        else if (x + 2 < StageWidth && stage[x + 2, z]) {
                            candidateX[i] = x;
                            candidateZ[i] = z;
                            ++i;
                        }
                        else if (z + 2 < StageHeight && stage[x, z + 2]) {
                            candidateX[i] = x;
                            candidateZ[i] = z;
                            ++i;
                        }
                    }
                }
            }
            if (i == 0) break;
            else {
                int rnd = Random.Range(0, i);
                Dig(candidateX[rnd], candidateZ[rnd]);
            }
        }
    }

    void Start() {

        initStage();

        for (int x = 0; x < StageWidth; x++) {
            for (int z = 0; z < StageHeight; z++) {

                if (!stage[x, z]) continue;

                Vector3 displacement = new Vector3(WallSize * x, 0, WallSize * z);

                GameObject obj = Instantiate(wall, transform.position + displacement, Quaternion.identity, this.transform);

                if ((x + z) % 2 == 0) obj.GetComponent<Renderer>().material.color = new Color32(86, 86, 86, 1);

            }
        }

        floor.transform.position = new Vector3(StageWidth / 2, -1, StageHeight / 2);
        floor.transform.localScale = new Vector3(StageWidth, 1, StageHeight);

        goal.transform.position = new Vector3(StageWidth - 2, -0.9f, StageHeight - 2);

    }
}
