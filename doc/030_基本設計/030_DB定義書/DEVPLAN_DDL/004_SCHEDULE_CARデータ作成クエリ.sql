-- ÀsPL/SQL
BEGIN

	/* XPW[pÔf[^í */
	DELETE FROM DEVPLAN.SCHEDULE_CAR;

	/* XPW[pÔ(±Ôúö)o^f[^æ¾ */
	FOR rec IN (
		SELECT DISTINCT
			A.CATEGORY_ID,
			A.CATEGORY,
			B.Ç[Ô
			FROM
				TESTCAR_SCHEDULE_ITEM A,
				(SELECT CATEGORY_ID, MAX(Ç[Ô) Ç[Ô FROM ±væ_O»Ôúö_Ô¼Xg WHERE Ç[Ô IS NOT NULL GROUP BY CATEGORY_ID) B
			WHERE
				A.CATEGORY_ID = B.CATEGORY_ID(+)
			ORDER BY
				A.CATEGORY_ID
	) LOOP

	INSERT INTO DEVPLAN.SCHEDULE_CAR (CATEGORY_ID,Ç[Ô,õl,INPUT_DATETIME,INPUT_PERSONEL_ID) VALUES (rec.CATEGORY_ID,rec.Ç[Ô,rec.CATEGORY,SYSDATE,'00001');

	END LOOP;

	/* XPW[pÔ(J[VFAúö)o^f[^æ¾ */
	FOR rec IN (
		SELECT DISTINCT
			A.CATEGORY_ID,
			A.CATEGORY,
			B.Ç[Ô
			FROM
				CARSHARING_SCHEDULE_ITEM A,
				(SELECT CATEGORY_ID, MAX(Ç[Ô) Ç[Ô FROM ±væ_O»Ôúö_Ô¼Xg WHERE Ç[Ô IS NOT NULL GROUP BY CATEGORY_ID) B
			WHERE
				A.CATEGORY_ID = B.CATEGORY_ID(+)
			ORDER BY
				A.CATEGORY_ID
	) LOOP

	INSERT INTO DEVPLAN.SCHEDULE_CAR (CATEGORY_ID,Ç[Ô,õl,INPUT_DATETIME,INPUT_PERSONEL_ID) VALUES (rec.CATEGORY_ID,rec.Ç[Ô,rec.CATEGORY,SYSDATE,'00001');

	END LOOP;

	/* XPW[pÔ(O»Ôúö)o^f[^æ¾ */
	FOR rec IN (
		SELECT DISTINCT
			A.CATEGORY_ID,
			A.CATEGORY,
			B.Ç[Ô
			FROM
				OUTERCAR_SCHEDULE_ITEM A,
				(SELECT CATEGORY_ID, MAX(Ç[Ô) Ç[Ô FROM ±væ_O»Ôúö_Ô¼Xg WHERE Ç[Ô IS NOT NULL GROUP BY CATEGORY_ID) B
			WHERE
				A.CATEGORY_ID = B.CATEGORY_ID(+)
			ORDER BY
				A.CATEGORY_ID
	) LOOP

	INSERT INTO DEVPLAN.SCHEDULE_CAR (CATEGORY_ID,Ç[Ô,õl,INPUT_DATETIME,INPUT_PERSONEL_ID) VALUES (rec.CATEGORY_ID,rec.Ç[Ô,rec.CATEGORY,SYSDATE,'00001');

	END LOOP;

	COMMIT;

EXCEPTION WHEN OTHERS THEN

	ROLLBACK;

END;
