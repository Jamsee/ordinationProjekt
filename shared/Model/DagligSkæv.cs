namespace shared.Model;

public class DagligSkæv : Ordination {
    public List<Dosis> doser { get; set; } = new List<Dosis>();

    public DagligSkæv(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen) {
	}

    public DagligSkæv(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel, Dosis[] doser) : base(laegemiddel, startDen, slutDen) {
        this.doser = doser.ToList();
    }    

    public DagligSkæv() : base(null!, new DateTime(), new DateTime()) {
    }

	public void opretDosis(DateTime tid, double antal) {
        doser.Add(new Dosis(tid, antal));
    }

	public override double samletDosis() {
		return base.antalDage() * doegnDosis();
	}

	public override double doegnDosis() {
        // TODO: Implement!
        //int patientId, int laegemiddelId, Dosis[] doser, DateTime startDato, DateTime slutDato
        int sumOfdoser = doser.Sum(x => Convert.ToInt32(x));
        double gennemsnitlig = sumOfdoser;//gydlig Ordinations periode
        return gennemsnitlig;
	}

	public override String getType() {
		return "DagligSkæv";
	}
}
