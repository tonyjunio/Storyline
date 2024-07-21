class ForeJect {
    #spawns = new Array();
    #surrounding = {
        x1: 0,
        y1: 0,
        x2: 1000,
        y2: 500
    };

    Spawn() {
        let dimension = this.#getDimension();
        let xyPos = this.#getXyPosition(dimension);
        let animationDuration = this.#getRndNumber(1, 4);

        let spwn = document.createElement("div");
        spwn.id = "";
        spwn.className = "foreject-spawn";
        spwn.style.top = xyPos.y + "px";
        spwn.style.left = xyPos.x + "px";
        spwn.style.height = dimension.h + "px";
        spwn.style.width = dimension.w + "px";

        let spwnpt = document.createElement("div");
        spwnpt.className = "point";

        // Render...
        spwn.appendChild(spwnpt)
        document.body.appendChild(spwn);

        // Attach animation and custom styles...
        spwnpt.className += " x";
        spwnpt.style.animationDuration = animationDuration + "s";
        spwnpt.style.backgroundColor = window.getRndRGBColorValue();

        // this.#spawns.push(spwn);
        this.#deSpawn(spwn, animationDuration * 1000);
    }

    #deSpawn(s, timeout) {
        window.setTimeout(() => {
            document.body.removeChild(s);
        }, timeout);
    }

    #getDimension() {
        // Currently just return a square dimension (same length for width and height).
        let length = this.#getRndNumber(30, 250);
        return {
            w: length,
            h: length
        };
    }

    #getXyPosition(spwnDimension) {
        let winWidth = window.innerWidth - (spwnDimension.w || 0);
        let winHeight = window.innerHeight - (spwnDimension.h || 0);

        return {
            x: this.#getRndNumber(0, winWidth),
            y: this.#getRndNumber(0, winHeight)
        };
    }

    #getRndNumber = (min, max) => Math.floor(Math.random(0) * (max - min + 1)) + min;

    //async #initAnims()
    //{
    //    for (let i = 0; i < this.#spawns.length; i++)
    //    {
    //        let spawn = this.#spawns[i];
    //        await this.#animateSpawns(spawn)
    //    }
    //}

    async #animateSpaw(s) {

    }
};

let foreject = new ForeJect();

window.setInterval(() => {
    foreject.Spawn();
}, 100);