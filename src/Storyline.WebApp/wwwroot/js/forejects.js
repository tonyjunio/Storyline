class ForeJect {
    #spawns = new Array();
    #surrounding = {
        x1: 0,
        y1: 0,
        x2: 1000,
        y2: 500
    };

    Spawn() {
        let xyPos = this.#getXyPosition();
        console.log(xyPos);

        let spwn = document.createElement("div");
        spwn.id = "";
        spwn.className = "foreject-spawn";
        spwn.style.top = xyPos.y + "px";
        spwn.style.left = xyPos.x + "px";

        let spwnpt = document.createElement("div");
        spwnpt.className = "point";

        // Render...
        spwn.appendChild(spwnpt)
        document.body.appendChild(spwn);

        this.#spawns.push(spwn);
        this.#deSpawn(spwn);
    }

    #deSpawn(spwn) {

    }

    #getXyPosition() {
        let fn = (min, max) => Math.floor(Math.random(0) * (max - min + 1)) + min;

        return {
            x: fn(10, 900),
            y: fn(10, 450)
        };
    }

    async #initAnims()
    {
        for (let i = 0; i < this.#spawns.length; i++)
        {
            let spawn = this.#spawns[i];
            await this.#animateSpawns(spawn)
        }
    }

    async #animateSpawns(s) {
        while (true) {

        }
    }
};

let foreject = new ForeJect();
foreject.Spawn();
