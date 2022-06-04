using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


class DecorationObstacle : MonoBehaviour,IPlaceableObj
    {

        //x of grid position
        protected int x;
        //z of grid position
        protected int z;

        protected int width;
        protected int height;

        public Vector2Int Position => throw new NotImplementedException();

        public bool IsObstacle { get => true; set { } }
        public Vector2Int Size
        {
            get
            {
                return new Vector2Int(width, height);
            }
            set
            {
                width = value.x;
                height = value.y;
            }
        }

        bool IPlaceableObj.IsObstacle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool placeAt(int x, int z)
        {
            bool isSucess = GridSystem.current.setValue(x, z, 100, this);
            if (isSucess)
            {
                Vector3 truePosition = GridSystem.current.getWorldPosition(x, z);
                transform.position = truePosition;

                GridSystem.current.removeValue(this.x, this.z, width, height);

                this.x = x;
                this.z = z;

                return true;
            }
            return false;
        }

        public bool placeAt(Vector3 worldPosition)
        {
            bool isSuccess = GridSystem.current.setValue(worldPosition, 100, this, width, height);
            if (isSuccess)
            {
                int x, z;
                GridSystem.current.getXZ(worldPosition, out x, out z);
                Vector3 truePosition = GridSystem.current.getWorldPosition(x, z);
                transform.position = truePosition;

                //GridSystem.current.removeValue(this.x, this.z, width, height);
                this.x = x;
                this.z = z;

                return true;
            }
            return false;
        }
    }

