INSERT INTO 
 gpðîñ(
  f[^ID
  , ðNO
  , SEQNO
  , ³FvR[h
  , STEPNO
  , ³Fóµ
  , ³FÒx
  , Ç³F
  , ú
  , ÇÓCÛ¼
  , ÇÓC¼
  , gpÛ¼
  , gp¼
  , ±àe
  , HæªNO
  , Às£
  , ÒWú
  , ÒWÒ
  , ÚÇæID
  , ÔêÔ
 )
SELECT
  A.f[^ID f[^ID
  , B.ðNO ðNO
  , '1' SEQNO
  , 'A' ³FvR[h
  , '0' STEPNO
  , 'Ï' ³Fóµ
  , NULL ³FÒx
  , NULL Ç³F
  , B.­sNú ú
  , G.SECTION_CODE ÇÓCÛ¼
  , F.SECTION_GROUP_CODE ÇÓC¼
  , E.SECTION_CODE gpÛ¼
  , D.SECTION_GROUP_CODE gp¼
  , 'óÌ' ±àe
  , B.HæªNO HæªNO
  , B.óÌs£ Às£
  , SYSDATE ÒWú
  , '000001' ÒWÒ
  , NULL ÚÇæID
  , NULL ÔêÔ 
FROM
  ( 
    select
      main.f[^ID 
    from
      ±Ôî{îñ main 
      LEFT JOIN gpðîñ SUB 
        ON main.f[^ID = SUB.f[^ID 
    where
      SUB.f[^ID is null
  ) A 
  inner JOIN ( 
    SELECT
      B2.* 
    FROM
      ( 
        SELECT
          f[^ID
          , MAX(ðNO) ðNO 
        FROM
          ±Ôðîñ 
        GROUP BY
          f[^ID
      ) B1 
      INNER JOIN ±Ôðîñ B2 
        ON B1.f[^ID = B2.f[^ID 
        AND B1.ðNO = B2.ðNO
  ) B 
    ON A.f[^ID = B.f[^ID 
    and B.Ç[­sL³ = 'Ï' 
  LEFT JOIN SECTION_GROUP_DATA D 
    ON B.óÌ = D.SECTION_GROUP_ID 
  LEFT JOIN SECTION_DATA E 
    ON D.SECTION_ID = E.SECTION_ID 
  LEFT JOIN SECTION_GROUP_DATA F 
    ON B.ÇÓC = F.SECTION_GROUP_ID 
  LEFT JOIN SECTION_DATA G 
    ON F.SECTION_ID = G.SECTION_ID 
ORDER BY
  A.f[^ID
  , B.ðNO;
