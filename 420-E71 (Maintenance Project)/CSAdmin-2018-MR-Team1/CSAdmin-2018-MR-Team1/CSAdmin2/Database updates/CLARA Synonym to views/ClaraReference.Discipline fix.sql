
Select
	p.*
from
	ClaraGrilles.Grille g
	left join ClaraProgrammes.Programme p
		on p.IDProgramme = g.IDProgramme
where
	p.IDTypeSanction < 11
order
	by p.Numero DESC